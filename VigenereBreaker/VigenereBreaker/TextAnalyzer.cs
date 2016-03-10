using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereBreaker
{
    class TextAnalyzer
    {
        public Dictionary<string, int> DetermineFrequencies(string text, int keyLength)
        {
            var frequencies = new Dictionary<string, int>();
            for (int i = 0; i < text.Length; i+=keyLength)
            {
                var substring = text.Substring(i, keyLength);
                if (!frequencies.ContainsKey(substring))
                {
                    frequencies.Add(substring, 1);
                }
                else
                {
                    frequencies[substring]++;
                }
            }
            return frequencies;
        } 
    }
}
