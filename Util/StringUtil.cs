using System;

namespace TabTextFinder.Util
{
    static class StringUtil
    {
        public static string GetFirstLine( string text )
        {
            // take the first line only
            int pos0 = text.IndexOf( '\r' );
            int pos1 = text.IndexOf( '\n' );
            int pos = (pos0 >= 0 && pos1 >= 0 && pos0 <= pos1) ? pos0 : pos1;
            if (pos >= 0) {
                return text.Substring( 0, pos );
            }
            return text;
        }

        public static string GetTimeSpanString( TimeSpan span )
        {
            long v = span.Ticks / 10000;
            if (v < 1000) {
                return String.Format( "{0} ms", v );
            }
            if (v < 10 * 1000) {
                return String.Format( "{0:F2} s", v / 1000.0 );
            }
            if (v < 100 * 1000) {
                return String.Format( "{0:F1} s", v / 1000.0 );
            }
            return String.Format( "{0} s", v / 1000 );
        }

        private static readonly string[] prefix = new string[] { "", "K", "M", "G", "T", "P" };
        public static string GetPrefixedString( long value )
        {
            int i = 1;
            for (; i < prefix.Length; ++i) {
                if (value < 1000) { break; }
                value /= 1000;
            }
            return String.Format( "{0} {1}", value, prefix[i - 1] );
        }
    }
}
