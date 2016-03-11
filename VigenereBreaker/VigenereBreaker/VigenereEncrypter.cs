using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereBreaker
{
    public class VigenereEncrypter
    {
        private const int LETTERS_IN_ALPHABET = 26;
        private const char FIRST_LETTER = 'a';
        private const char LAST_LETTER = (char)(FIRST_LETTER + LETTERS_IN_ALPHABET);

        public VigenereEncrypter()
        {
            
        }

        public string Encrypt(string key, string plainText)
        {
            return MapFunc(key, plainText, EncryptChar);
        }

        public string Decrypt(string key, string plainText)
        {
            return MapFunc(key, plainText, DecryptChar);
        }

        private string MapFunc(string key, string text, Func<char, char, char> encrypt)
        {
            if (string.IsNullOrEmpty(key))
            {
                return "Key must have letters.";
            }
            text = text.ToLower();
            key = key.ToLower();
            var encipheredText = new StringBuilder();
            for (int textIndex = 0; textIndex < text.Length; textIndex++)
            {
                int keyIndex = textIndex % key.Length;
                char keyLetter = key[keyIndex];
                char plainTextLetter = text[textIndex];
                char cipherLetter = encrypt(keyLetter, plainTextLetter);
                encipheredText.Append(cipherLetter);
            }
            return encipheredText.ToString();
        }

        public char CalculateKey(char plainTextLetter, char cipherLetter)
        {
            int plainTextLetterAlphaIndex = AlphaIndex(plainTextLetter);
            int cipherLetterAlphaIndex = AlphaIndex(cipherLetter);
            int keyLetterIndex = cipherLetterAlphaIndex - plainTextLetterAlphaIndex;
            if (keyLetterIndex < 0)
            {
                keyLetterIndex += LETTERS_IN_ALPHABET;
            }
            char keyLetter = CharAtAlphaIndex(keyLetterIndex);
            return keyLetter;
        }

        public char EncryptChar(char keyLetter, char plainTextLetter)
        {
            int plainTextLetterAlphaIndex = AlphaIndex(plainTextLetter);
            int keyLetterAlphaIndex = AlphaIndex(keyLetter);
            int cipherLetterIndex = (plainTextLetterAlphaIndex + keyLetterAlphaIndex) % LETTERS_IN_ALPHABET;
            char cipherLetter = CharAtAlphaIndex(cipherLetterIndex);
            return cipherLetter;
        }

        public char DecryptChar(char keyLetter, char cipherLetter)
        {
            int keyLetterAlphaIndex = AlphaIndex(keyLetter);
            int cipherLetterAlphaIndex = AlphaIndex(cipherLetter);
            int plainTextLetterAlphaIndex = cipherLetterAlphaIndex - keyLetterAlphaIndex;
            if(plainTextLetterAlphaIndex < 0)
            {
                plainTextLetterAlphaIndex += LETTERS_IN_ALPHABET;
            }
            char plainTextLetter = CharAtAlphaIndex(plainTextLetterAlphaIndex);
            return plainTextLetter;
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
    }
}
