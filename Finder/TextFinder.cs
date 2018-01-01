using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using TabTextFinder.Util.PInvokeAPI;

namespace TabTextFinder.Finder
{
    class TextFinder
    {
        public bool Abort { get; set; }

        public string Text { get; private set; }
        public FindTextOption Option { get; private set; }

        private readonly Regex regex;
        private readonly StringComparison compare;

        private delegate int Finder( string line, int start, ref int match );

        private readonly Finder finder;
        private readonly ITextFindEngine engine;
        //private readonly bool use_native_code;

        public TextFinder( string text, FindTextOption option )
        {
            Text = text;
            Option = option;

            if (option.Regx) {
                finder = FindByRegex;
                regex = new Regex( text, (option.Case) ? RegexOptions.None : RegexOptions.IgnoreCase );
            } else if (!Option.Case) {
                finder = FindBySystem;
                compare = (option.Case) ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            } else {
                finder = FindByEngine;
                engine = new BMFindEngine( text );
            }

            //use_native_code = !KeyState.IsPressedAsync( Keys.ControlKey );
        }

        private int FindBySystem( string content, int start, ref int match )
        {
            match = Text.Length;
            return content.IndexOf( Text, start, compare );
        }

        private int FindByEngine( string content, int start, ref int match )
        {
            match = Text.Length;
            return engine.IndexIn( content, start, content.Length );
        }

        private int FindByRegex( string content, int start, ref int match )
        {
            Match m = regex.Match( content, start );
            if (m.Success) {
                match = m.Length;
                return m.Index;
            }
            return -1;
        }

        public bool Find( string content, int start, ref int index, ref int length )
        {
            int end = content.Length;
            while (start < end) {
                index = finder( content, start, ref length );
                if (index < start) { break; }
                if (!Option.Word || FindUtil.IsOnWordBoundary( content, index, length )) {
                    return true;
                }
                start = index + 1;
            }
            return false;
        }

        // find all matches given a single line
        public FoundLineCollection FindInSingleLine( string line )
        {
            FoundLineCollection FoundLines = new FoundLineCollection();

            int index = 0;
            int length = 0;
            int pos = 0;
            while (Find( line, pos, ref index, ref length )) {
                FoundLines.Add( new FoundLine( index, length ) );
                pos = index + length;
            }

            return FoundLines;
        }

        // find all lines that contain at least one match
        public FoundLineCollection FindInLines( TextReader reader, string content, out int num_lines )
        {
            FoundLineCollection FoundLines = new FoundLineCollection();
            //if (use_native_code) {// native implementation
            //    BMFindEngine bme = engine as BMFindEngine;
            //    if (bme != null && content != null) {
            //        int[] result = null;
            //        num_lines = NTextFinder.Native.BMFinder.FindInLines( Text, content, bme.skip, bme.last_idx_of, (Option.Word) ? FindUtil.IsWordTable : null, ref result );
            //        int n = 0;
            //        while (n + 4 <= result.Length) {
            //            int line = result[n++];
            //            int begin = result[n++];
            //            int len = result[n++];
            //            int idx = result[n++];
            //            FoundLines.Add( new FoundLine( line, idx, Text.Length, content.Substring( begin, len ) ) );
            //        }
            //        return FoundLines;
            //    }
            //}

            int index = 0;
            int length = 0;
            int num = 0;
            while (!Abort) {
                string line = reader.ReadLine();
                if (line == null) { break; }

                if (Find( line, 0, ref index, ref length )) {
                    FoundLines.Add( new FoundLine( num, index, length, line ) );
                }
                num++;
            }

            num_lines = num;
            return FoundLines;
        }
    }

    static class FindUtil
    {
        public static byte[] IsWordTable;
        private static BitArray isWordMap;

        static FindUtil()
        {
            isWordMap = new BitArray( 0x10000 );
            for (int ch = 0; ch < 0x10000; ++ch) {
                isWordMap[ch] = isWord( (char) ch );
            }

            IsWordTable = new byte[0x10000 / 8];
            isWordMap.CopyTo( IsWordTable, 0 );
        }

        public static bool IsOnWordBoundary( string str, int index, int length )
        {
            return IsOnWordBoundary( str, 0, index, str.Length, length );
        }

        public static bool IsOnWordBoundary( string str, int begin, int index, int end, int length )
        {
            if (length == 0) { return false; }

            // check the beginning
            if (index > begin) {
                bool w0 = IsWord( str[index - 1] );
                bool w1 = IsWord( str[index] );
                if (w0 == w1) { return false; }
            }
            // cehck the ending
            if (index + length < end) {
                bool w0 = IsWord( str[index + length - 1] );
                bool w1 = IsWord( str[index + length] );
                if (w0 == w1) { return false; }
            }
            return true;
        }

        private static bool IsWord( char ch )
        {
            return isWord( ch );
            //return isWordMap[ch];
        }

        private static bool isWord( char ch )
        {
            UnicodeCategory c = char.GetUnicodeCategory( ch );
            return c == UnicodeCategory.LowercaseLetter
                || c == UnicodeCategory.UppercaseLetter
                || c == UnicodeCategory.TitlecaseLetter
                || c == UnicodeCategory.OtherLetter
                || c == UnicodeCategory.DecimalDigitNumber
                || c == UnicodeCategory.ConnectorPunctuation
                || c == UnicodeCategory.ModifierLetter;
        }
    }
}
