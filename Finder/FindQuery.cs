using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using TabTextFinder.Util.PInvokeAPI;

namespace TabTextFinder.Finder
{
    enum FindType
    {
        NewPath,
        FoundPath,
        FilterInclude,
        FilterExclude,
    }

    struct FindTextOption
    {
        public bool Case { get { return _case; } }  // true if case sensitive search
        public bool Word { get { return _word; } }  // true if whole word search
        public bool Regx { get { return _regx; } }  // true if use regular expression

        private readonly bool _case;
        private readonly bool _word;
        private readonly bool _regx;

        public FindTextOption( bool case_, bool word, bool regx )
        {
            _case = case_;
            _word = word;
            _regx = regx;
        }
    }

    sealed class FoundFile
    {
        public FileInfo Info { get; private set; }
        public string Encoding { get; private set; }
        public bool EncodingForced { get; private set; }

        public FoundFile( FileInfo info, string encoding, bool encoding_forced )
        {
            Info = info;
            Encoding = encoding;
            EncodingForced = encoding_forced;
        }
    }

    // for VirtualListView, store data as ListViewItem
    sealed class FoundLine
    {
        public int FileIdx { get; set; }
        public string FileName { get; set; }

        public int Line { get; private set; }
        public int Column { get; private set; }
        public int Length { get; private set; }
        public string Content { get; private set; }
        public string Preview { get { return (preview == null) ? Content : preview; } }

        private string preview;
        private ListViewItem item;

        public FoundLine( int column, int length )
        {
            Column = column;
            Length = length;
        }

        public FoundLine( int line, int column, int length, string content )
        {
            Line = line;
            Column = column;
            Length = length;
            Content = content;
        }

        public void MakePreview( FoundLineOption opt )
        {
            if (Content == null) { return; }
            int first = Math.Max( 0, Column - opt.MaxCharsLeft );
            int last = Math.Min( Content.Length, Column + Length + opt.MaxCharsRight );
            preview = string.Format( "{0}{1}{2}", (first == 0) ? "" : "...", Content.Substring( first, last - first ), (last == Content.Length) ? "" : "..." );
        }

        public ListViewItem Item
        {
            get
            {
                if (item == null) {
                    item = new ListViewItem( FileName );
                    item.SubItems.Add( String.Format( "{0:N0}", Line + 1 ) );
                    item.SubItems.Add( String.Format( "{0:N0}", Column + 1 ) );
                    item.SubItems.Add( Preview );
                }
                return item;
            }
        }
    }

    class FoundFileCollection : List<FoundFile> { }
    class FoundLineCollection : List<FoundLine> { }

    sealed class FoundLineOption
    {
        public bool ShowFullPath { get; private set; }
        public int MaxCharsLeft { get; private set; }
        public int MaxCharsRight { get; private set; }

        public FoundLineOption( bool full_path, int left_chars, int right_chars )
        {
            ShowFullPath = full_path;
            MaxCharsLeft = left_chars;
            MaxCharsRight = right_chars;
        }
    }

    sealed class FindStatus
    {
        public int TotalFiles { get { return files; } }
        public int TotalLines { get { return lines; } }
        public long TotalCounts { get { return counts; } }
        public TimeSpan Elapsed { get; private set; }
        public int FoundLinesCount { get { return found_lines; } }

        private Stopwatch watch = Stopwatch.StartNew();

        private int files;
        private int lines;
        private long counts;        // bytes or chars
        private int found_lines;

        public void Update( int _lines, long _counts, int _found_lines )
        {
            Interlocked.Increment( ref files );
            Interlocked.Add( ref lines, _lines );
            Interlocked.Add( ref counts, _counts );
            Interlocked.Add( ref found_lines, _found_lines );
            Elapsed = watch.Elapsed;
        }
    }

    abstract class FindQuery
    {
        public bool Abort { get; set; }
        public bool Working { get; protected set; }

        public FindType Type { get; private set; }
        public TextFinder Finder { get; private set; }
        public TextFinder KeywordFinder { get; private set; }

        public FindStatus Status { get; private set; }
        public FoundFileCollection FoundFiles { get; private set; }
        public FoundLineCollection FoundLines { get; private set; }

        private FoundLineOption line_option;

        protected int NumThreads { get; private set; }

        public FindQuery( FindType type, FoundLineOption option, TextFinder finder, TextFinder keyword_finder )
        {
            Abort = false;
            Working = true;

            Type = type;
            Finder = finder;
            KeywordFinder = keyword_finder;

            Status = new FindStatus();
            FoundFiles = new FoundFileCollection();
            FoundLines = new FoundLineCollection();
            line_option = option;

            NumThreads = SystemInfo.NumberOfProcessors;
        }

        public abstract void Find();

        protected void AppendFoundForFile( FoundFile file, FoundLineCollection lines, int num_lines, long num_counts )
        {
            if (file != null && lines.Count > 0) {
                lock (FoundFiles) {
                    // add file if Text was found
                    FoundFiles.Add( file );
                    {// add lines
                        int idx = FoundFiles.Count - 1;
                        string name = file.Info.FullName;
                        if (line_option.ShowFullPath == false) {
                            name = Path.GetFileName( name );
                        }
                        foreach (FoundLine line in lines) {
                            line.FileIdx = idx;
                            line.FileName = name;
                            line.MakePreview( line_option );
                        }
                    }
                    FoundLines.AddRange( lines );
                }
            }
            Status.Update( num_lines, num_counts, lines.Count );
        }
    }
}
