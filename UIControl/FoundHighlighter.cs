using System;
using System.Collections.Generic;
using System.IO;
using Sgry.Azuki;
using Sgry.Azuki.Highlighter;
using TabTextFinder.Finder;

namespace TabTextFinder.UIControl
{
    class FoundHighLighter : IHighlighter
    {
        public bool CanUseHook { get { return false; } }
        public HighlightHook HookProc { get; set; }

        private readonly IHighlighter DefaultHilighter;
        private readonly FoundLineCollection FoundLines;
        private readonly TextFinder Finder;
        private readonly int FileIdx;

        private static Dictionary<IHighlighter, string> hilighters;

        static FoundHighLighter()
        {
            hilighters = new Dictionary<IHighlighter, string>();
            hilighters.Add( Highlighters.CSharp, ".cs" );
            hilighters.Add( Highlighters.Latex, ".tex" );
            hilighters.Add( Highlighters.Java, ".java" );
            hilighters.Add( Highlighters.Ruby, ".rb" );
            hilighters.Add( Highlighters.Cpp, ".c.cpp.cxx.h.hpp.hxx" );
            hilighters.Add( Highlighters.Xml, ".xml.xsl.htm.html" );
        }

        private static IHighlighter getDefaultHilighter( string path )
        {
            string ext = Path.GetExtension( path );
            foreach (KeyValuePair<IHighlighter, string> pair in hilighters) {
                if (pair.Value.IndexOf( ext, StringComparison.CurrentCultureIgnoreCase ) >= 0) {
                    return pair.Key;
                }
            }
            return null;
        }

        public FoundHighLighter( string path, int file_idx, FoundLineCollection found_lines, TextFinder finder )
        {
            DefaultHilighter = getDefaultHilighter( path );
            FileIdx = file_idx;
            FoundLines = found_lines;
            Finder = finder;
        }

        public void Highlight( Document doc )
        {
            if (DefaultHilighter != null) {
                DefaultHilighter.Highlight( doc );
            }
            int begin = 0;
            int end = Int32.MaxValue;
            highlight( doc, ref begin, ref end );
        }

        public void Highlight( Document doc, ref int dirtyBegin, ref int dirtyEnd )
        {
            // Called from UIimpl in a multi-thread context
            // and causes an asynch problem when doc is changed frequently.
            // And, Highlight( doc ) is enough to highlight the document which is called
            //  in the synchronous way when doc.Highlighter is set.
            return;
        }

        private void highlight( Document doc, ref int dirtyBegin, ref int dirtyEnd )
        {
            int begin = Int32.MaxValue;
            int end = 0;
            // foreach cannot be used here as matches can be changed asynchronously
            int count = FoundLines.Count;
            for (int i = 0; i < count; ++i) {
                FoundLine fl = FoundLines[i];
                if (fl.FileIdx != FileIdx) { continue; }

                int line = fl.Line;
                {
                    if (line >= doc.LineCount) { continue; }
                    if (line < dirtyBegin || dirtyEnd < line) { continue; }
                    begin = Math.Min( begin, line );
                    end = Math.Max( end, line );
                }

                int length = doc.GetLineLength( line );
                int idx = doc.GetCharIndexFromLineColumnIndex( line, 0 );
                {// hilight the whole line first
                    SetCharClassRange( doc, idx, length, CharClass.Heading6 );
                }
                {// hilight the found word(s) by searching for the word in "text"
                    // use the azuki content (but not the listview content)
                    string text = doc.GetTextInRange( line, 0, line, length );
                    foreach (FoundLine found in Finder.FindInSingleLine( text )) {
                        SetCharClassRange( doc, idx + found.Column, found.Length, CharClass.Heading5 );
                    }
                }
            }
            dirtyBegin = begin;
            dirtyEnd = end;
        }

        private void SetCharClassRange( Document doc, int idx, int length, CharClass klass )
        {
            for (int n = 0; n < length; ++n) {
                doc.SetCharClass( idx + n, klass );
            }
        }
    }
}
