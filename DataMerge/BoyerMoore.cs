using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMerge
{
    /// <summary>
    /// Class that implements Boyer-Moore and related exact string-matching algorithms
    /// </summary>
    /// <remarks>
    /// From "Handbook of exact string-matching algorithms"
    ///   by Christian Charras and Thierry Lecroq
    ///   chapter 15
    /// http://www-igm.univ-mlv.fr/~lecroq/string/node15.html#SECTION00150
    /// </remarks>
    public class BoyerMoore
    {
        private int[] m_badCharacterShift;
        private int[] m_goodSuffixShift;
        private int[] m_suffixes;
        private string m_pattern;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pattern">Pattern for search</param>
        public BoyerMoore(string pattern)
        {
            /* Preprocessing */
            m_pattern = pattern;
            m_badCharacterShift = BuildBadCharacterShift(pattern);
            m_suffixes = FindSuffixes(pattern);
            m_goodSuffixShift = BuildGoodSuffixShift(pattern, m_suffixes);
        }

        /// <summary>
        /// Build the bad character shift array.
        /// </summary>
        /// <param name="pattern">Pattern for search</param>
        /// <returns>bad character shift array</returns>
        private int[] BuildBadCharacterShift(string pattern)
        {
            int[] badCharacterShift = new int[256];

            for (int c = 0; c < badCharacterShift.Length; ++c)
                badCharacterShift[c] = pattern.Length;
            for (int i = 0; i < pattern.Length - 1; ++i)
                badCharacterShift[pattern[i]] = pattern.Length - i - 1;

            return badCharacterShift;
        }

        /// <summary>
        /// Find suffixes in the pattern
        /// </summary>
        /// <param name="pattern">Pattern for search</param>
        /// <returns>Suffix array</returns>
        private int[] FindSuffixes(string pattern)
        {
            int f = 0, g;

            int patternLength = pattern.Length;
            int[] suffixes = new int[pattern.Length + 1];

            suffixes[patternLength - 1] = patternLength;
            g = patternLength - 1;
            for (int i = patternLength - 2; i >= 0; --i)
            {
                if (i > g && suffixes[i + patternLength - 1 - f] < i - g)
                    suffixes[i] = suffixes[i + patternLength - 1 - f];
                else
                {
                    if (i < g)
                        g = i;
                    f = i;
                    while (g >= 0 && (pattern[g] == pattern[g + patternLength - 1 - f]))
                        --g;
                    suffixes[i] = f - g;
                }
            }

            return suffixes;
        }

        /// <summary>
        /// Build the good suffix array.
        /// </summary>
        /// <param name="pattern">Pattern for search</param>
        /// <returns>Good suffix shift array</returns>
        private int[] BuildGoodSuffixShift(string pattern, int[] suff)
        {
            int patternLength = pattern.Length;
            int[] goodSuffixShift = new int[pattern.Length + 1];

            for (int i = 0; i < patternLength; ++i)
                goodSuffixShift[i] = patternLength;
            int j = 0;
            for (int i = patternLength - 1; i >= -1; --i)
                if (i == -1 || suff[i] == i + 1)
                    for (; j < patternLength - 1 - i; ++j)
                        if (goodSuffixShift[j] == patternLength)
                            goodSuffixShift[j] = patternLength - 1 - i;
            for (int i = 0; i <= patternLength - 2; ++i)
                goodSuffixShift[patternLength - 1 - suff[i]] = patternLength - 1 - i;

            return goodSuffixShift;
        }

        /// <summary>
        /// Return all matches of the pattern in specified text using the Boyer-Moore algorithm
        /// </summary>
        /// <param name="text">text to be searched</param>
        /// <param name="startingIndex">Index at which search begins</param>
        /// <returns>IEnumerable which returns the indexes of pattern matches</returns>
        public IEnumerable<int> BoyerMooreMatch(string text, int startingIndex)
        {
            int patternLength = m_pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startingIndex;
            while (index <= textLength - patternLength)
            {
                int unmatched;
                for (unmatched = patternLength - 1;
                  unmatched >= 0 && (m_pattern[unmatched] == text[unmatched + index]);
                  --unmatched
                ) ; // empty

                if (unmatched < 0)
                {
                    yield return index;
                    index += m_goodSuffixShift[0];
                }
                else
                    index += Math.Max(m_goodSuffixShift[unmatched],
                      m_badCharacterShift[text[unmatched + index]] - patternLength + 1 + unmatched);
            }
        }

        /// <summary>
        /// Return True or False for matching the search text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startingIndex"></param>
        /// <returns>True if a match is found, False otherwise</returns>
        public bool BoyerMooreMatchAny(string text, int startingIndex)
        {
            int patternLength = m_pattern.Length;
            int textLength = text.Length;

            /* Searching */
            int index = startingIndex;
            while (index <= textLength - patternLength)
            {
                int unmatched;
                for (unmatched = patternLength - 1;
                  unmatched >= 0 && (m_pattern[unmatched] == text[unmatched + index]);
                  --unmatched
                ) ; // empty

                if (unmatched < 0)
                {
                    return true;
                }
                else
                    index += Math.Max(m_goodSuffixShift[unmatched],
                      m_badCharacterShift[text[unmatched + index]] - patternLength + 1 + unmatched);
            }
            return false;
        }

        /// <summary>
        /// Return all matches of the pattern in specified text using the Boyer-Moore algorithm
        /// </summary>
        /// <param name="text">text to be searched</param>
        /// <returns>IEnumerable which returns the indexes of pattern matches</returns>
        public bool BoyerMooreMatch(string text)
        {
            return BoyerMooreMatchAny(text, 0);
        }
    }
}