using System;
using System.Threading;

namespace TabTextFinder.Finder
{
    sealed class FindFilterParam
    {
        public FindQuery BaseQuery { get; private set; }

        public FindFilterParam( FindQuery base_query )
        {
            BaseQuery = base_query;
        }
    }

    class FindQueryByFilter : FindQuery
    {
        private FindFilterParam param;

        private const bool async = true;
        private readonly int[] start_idx;   // the starting line index for files

        public FindQueryByFilter( FindType type, FoundLineOption line_option, TextFinder finder, TextFinder keyword_finder, FindFilterParam _param )
            : base( type, line_option, finder, keyword_finder )
        {
            param = _param;
            InitFileLineIndex( param.BaseQuery, out start_idx );
        }

        private static void InitFileLineIndex( FindQuery query, out int[] index )
        {
            int num_files = query.FoundFiles.Count;
            int num_lines = query.FoundLines.Count;
            index = new int[num_files + 1];
            {
                int prev = -1;
                for (int line = 0; line < num_lines; ++line) {
                    int file = query.FoundLines[line].FileIdx;
                    if (prev != file) {
                        for (int i = prev + 1; i <= file; ++i) {
                            index[i] = line;
                        }
                        prev = file;
                    }
                }
            }
            index[num_files] = num_lines;
        }

        public override void Find()
        {
            if (async) {
                Action<int>[] actions = new Action<int>[NumThreads];
                IAsyncResult[] ars = new IAsyncResult[NumThreads];
                WaitHandle[] handles = new WaitHandle[NumThreads];
                for (int i = 0; i < NumThreads; ++i) {
                    actions[i] = new Action<int>( FilterFile );
                    ars[i] = actions[i].BeginInvoke( i, null, null );
                    handles[i] = ars[i].AsyncWaitHandle;
                }
                WaitHandle.WaitAll( handles );
                for (int i = 0; i < NumThreads; ++i) {
                    actions[i].EndInvoke( ars[i] );
                }
            } else {
                FilterFile( 0 );
            }
            Working = false;
        }

        private void FilterFile( int offset )
        {
            for (int file = 0; file + 1 < start_idx.Length; ++file) {
                if (file % NumThreads == offset) {
                    FilterLine( file, start_idx[file], start_idx[file + 1] );
                }
            }
        }

        private void FilterLine( int file_idx, int line_begin, int line_end )
        {
            FoundLineCollection lines = new FoundLineCollection();
            int index = 0;
            int length = 0;
            long num_chars = 0;
            for (int idx = line_begin; idx < line_end; ++idx) {
                if (Abort) { break; }
                FoundLine fl = param.BaseQuery.FoundLines[idx];
                num_chars += fl.Content.Length;
                bool ret = Finder.Find( fl.Content, 0, ref index, ref length );
                if (Type == FindType.FilterInclude && ret) {
                    lines.Add( new FoundLine( fl.Line, index, length, fl.Content ) );
                } else if (Type == FindType.FilterExclude && !ret) {
                    lines.Add( new FoundLine( fl.Line, fl.Column, fl.Length, fl.Content ) );
                }
            }

            int num_lines = line_end - line_begin;
            FoundFile file = param.BaseQuery.FoundFiles[file_idx];
            AppendFoundForFile( file, lines, num_lines, num_chars );
        }
    }
}
