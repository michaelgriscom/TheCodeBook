using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereBreaker
{
    class VigenereEncrypter
    {
        private const int LETTERS_IN_ALPHABET = 26;
        private const char FIRST_LETTER = 'a';
        private const char LAST_LETTER = (char)(FIRST_LETTER + LETTERS_IN_ALPHABET);

        public VigenereEncrypter()
        {
            
        }

        public string Encrypt(string key, string plainText)
        {
            if (string.IsNullOrEmpty(key))
            {
                return "Key must have letters.";
            }
            plainText = plainText.ToLower();
            key = key.ToLower();
            var encipheredText = new StringBuilder();
            for (int plainTextIndex = 0; plainTextIndex < plainText.Length; plainTextIndex++)
            {
                int keyIndex = plainTextIndex % key.Length;
                char keyLetter = key[keyIndex];
                char plainTextLetter = plainText[plainTextIndex];

                int plainTextAlphaIndex = AlphaIndex(plainTextLetter);
                int keyLetterAlphaIndex = AlphaIndex(keyLetter);
                int cipherLetterIndex = (plainTextAlphaIndex + keyLetterAlphaIndex) % LETTERS_IN_ALPHABET;
                char cipherLetter = CharAtAlphaIndex(cipherLetterIndex);
                encipheredText.Append(cipherLetter);
            }
            return encipheredText.ToString();
        }

        private static int AlphaIndex(char letter)
        {
            if (letter < FIRST_LETTER || letter > LAST_LETTER)
            {
                throw new Exception(string.Format("Letter must be {0}-{1}.", FIRST_LETTER, LAST_LETTER));
            }
            return letter - FIRST_LETTER;
        }

        private static char CharAtAlphaIndex(int alphaIndex)
        {
            var firstLetterAlphaIndex = AlphaIndex(FIRST_LETTER);
            var lastLetterAlphaIndex = AlphaIndex(LAST_LETTER);
            if (alphaIndex < firstLetterAlphaIndex || alphaIndex > lastLetterAlphaIndex)
            {
                throw new Exception(string.Format("Index must be {0}-{1}.", firstLetterAlphaIndex, lastLetterAlphaIndex));
            }
            return (char)(FIRST_LETTER + alphaIndex);
        }

        public string Decrypt(string key, string cipherText)
        {
            if (string.IsNullOrEmpty(key))
            {
                return "Key must have letters.";
            }
            return "";
        }
    }
}
