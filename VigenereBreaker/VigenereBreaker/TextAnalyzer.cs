#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace VigenereBreaker
{
    public class TextAnalyzer
    {
        private const int LETTERS_IN_ALPHABET = 26;

        public IEnumerable<DataGridItem> DetermineFrequencies(string text, int keyLength)
        {
            text = Regex.Replace(text, @"\s+", ""); // remove whitespace

            var freqs = new List<DataGridItem>();
            for (int i = 0; i < keyLength; i++)
            {
                for (int j = i; j < text.Length; j += keyLength)
                {
                    for (int k = 1; k <= keyLength && j + k < text.Length; k++)
                    {
                        string substring = text.Substring(j, k);
                        var freq = freqs.FirstOrDefault(x => x.String == substring && x.KeyIndex == i);
                        if (freq == null)
                        {
                            freq = new DataGridItem
                            {
                                Frequency = 1,
                                KeyIndex = i,
                                String = substring,
                                ExpectedFrequency =
                                    text.Length/(float) (keyLength*(Math.Pow(LETTERS_IN_ALPHABET, substring.Length)))
                            };
                            freqs.Add(freq);
                        }
                        else
                        {
                            freq.Frequency++;
                        }
                    }
                }
            }
            return freqs;
        }

        public class DataGridItem
        {
            public int KeyIndex { get; set; }
            public string String { get; set; }
            public int Frequency { get; set; }
            public float ExpectedFrequency { get; set; }

            public float Abnormality
            {
                get { return Frequency/ExpectedFrequency; }
            }
        }
    }
}