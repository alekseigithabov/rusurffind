using System;

namespace TabTextFinder.Finder
{
    interface ITextFindEngine
    {
        int IndexIn( string content, int begin, int end );
    }

    class BMFindEngine : ITextFindEngine
    {
        private string text;
        public int[] skip;			// Array of shifts with self-substring match check
        public int[] last_idx_of;	// Array of last occurrence of each character

        public BMFindEngine( string text )
        {
            this.text = text;
            Prepare( text );
        }

        // This helper function checks whether the last "portion" bytes of "text" exist
        // within the "text" at "offset" (counted from the end of the string),
        // and whether the character preceding "offset" is not a match.
        // Notice that the range being checked may reach beyond the
        // beginning of the string. Such range is ignored.
        private static bool bm_needlematch( string text, int start, int offset )
        {
            int n0 = start - offset;
            int n1 = start;

            if (n0 > 0 && text[n0 - 1] == text[n1 - 1]) {
                return false;
            }

            int ignore = 0;
            if (n0 < 0) {
                ignore = -n0;
                n0 = 0;
            }

            int len = text.Length - start - ignore;
            return (text.Substring( n1 + ignore, len ) == text.Substring( n0, len ));
        }

        private void Prepare( string text )
        {
            // Preprocess #1: init last_idx_of[]
            last_idx_of = new int[256 * 256];

            // Initialize the table to default value
            for (int i = 0; i < last_idx_of.Length; ++i) {
                last_idx_of[i] = -1;
            }

            // Then populate it with the analysis of the value, but ignoring the last letter
            for (int i = 0; i < text.Length - 1; ++i) {
                last_idx_of[text[i]] = i;
            }

            // Preprocess #2: init skip[]
            skip = new int[text.Length];

            // Note: This step could be made a lot faster.
            // A simple implementation is shown here.
            for (int start = 0; start < text.Length; ++start) {
                int offset = 0;
                while (offset < text.Length && !bm_needlematch( text, start + 1, offset )) {
                    ++offset;
                }
                skip[start] = offset;
            }
        }

        public int IndexIn( string content, int begin, int end )
        {
            int text_length = text.Length;
            if (end - begin < text_length) { return -1; }

            int pos = begin;
            while (pos + text_length <= end) {
                int rpos = text_length - 1;
                while (text[rpos] == content[pos + rpos]) {
                    if (rpos == 0) { return pos; }
                    rpos--;
                }
                pos += Math.Max( skip[rpos], rpos - last_idx_of[content[pos + rpos]] );
            }
            return -1;
        }
    }

    class TBMFindEngine : ITextFindEngine
    {
        private string text;
        private int text_length;

        private const int ASIZE = 256 * 256;
        private int[] bmBc = new int[ASIZE];
        private int[] bmGs = new int[ASIZE];

        public TBMFindEngine( string text )
        {
            this.text = text;
            text_length = text.Length;

            preBmGs( text );
            preBmBc( text );
        }

        private int BM( string content )
        {
            int length = content.Length;

            // Searching
            int pos = 0;
            while (pos <= length - text_length) {
                int rpos = text_length - 1;
                while (rpos >= 0 && text[rpos] == content[rpos + pos]) {
                    rpos--;
                }
                if (rpos < 0) {
                    return pos;
                } else {
                    pos += Math.Max( bmGs[rpos], bmBc[content[rpos + pos]] - text_length + 1 + rpos );
                }
            }

            return -1;
        }

        public int IndexIn( string content, int begin, int end )
        {
            int length = end - begin;

            // Searching
            int u = 0;
            int shift = text_length;

            int pos = begin;
            while (pos + text_length <= end) {
                int rpos = text_length - 1;
                while (rpos >= 0 && text[rpos] == content[pos + rpos]) {
                    rpos--;
                    if (u != 0 && rpos == text_length - 1 - shift) {
                        rpos -= u;
                    }
                }
                if (rpos < 0) { return pos; }

                int v = text_length - 1 - rpos;
                int turboShift = u - v;
                int bcShift = bmBc[content[pos + rpos]] - text_length + 1 + rpos;   // original BM method
                shift = Math.Max( turboShift, bcShift );
                shift = Math.Max( shift, bmGs[rpos] );
                if (shift == bmGs[rpos]) {
                    u = Math.Min( text_length - shift, v );
                } else {
                    if (turboShift < bcShift) {
                        shift = Math.Max( shift, u + 1 );
                    }
                    u = 0;
                }
                pos += shift;
            }
            return -1;
        }

        private void preBmBc( string x )
        {
            for (int i = 0; i < ASIZE; ++i) {
                bmBc[i] = text_length;
            }
            for (int i = 0; i < text_length - 1; ++i) {
                bmBc[x[i]] = text_length - i - 1;
            }
        }

        private void suffixes( string x, int[] suff )
        {
            suff[text_length - 1] = text_length;
            int f = 0;
            int g = text_length - 1;
            for (int i = text_length - 2; i >= 0; --i) {
                if (i > g && suff[i + text_length - 1 - f] < i - g) {
                    suff[i] = suff[i + text_length - 1 - f];
                } else {
                    if (i < g) {
                        g = i;
                    }
                    f = i;
                    while (g >= 0 && x[g] == x[g + text_length - 1 - f]) {
                        --g;
                    }
                    suff[i] = f - g;
                }
            }
        }

        private void preBmGs( string x )
        {
            int m = x.Length;
            int[] suff = new int[m];

            suffixes( x, suff );

            for (int i = 0; i < m; ++i) {
                bmGs[i] = m;
            }
            int j = 0;
            for (int i = m - 1; i >= 0; --i) {
                if (suff[i] == i + 1) {
                    for (; j < m - 1 - i; ++j) {
                        if (bmGs[j] == m) {
                            bmGs[j] = m - 1 - i;
                        }
                    }
                }
            }
            for (int i = 0; i <= m - 2; ++i) {
                bmGs[m - 1 - suff[i]] = m - 1 - i;
            }
        }
    }
}
