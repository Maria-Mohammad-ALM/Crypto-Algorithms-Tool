using System;

namespace EncryptionSuite.Ciphers
{
    public static class Vigenere
    {
        public static string Encrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty.");

            text = text.ToUpper();
            key = key.ToUpper();
            string result = string.Empty;
            int keyIndex = 0;

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    int shift = key[keyIndex] - 'A';
                    result += (char)('A' + (c - 'A' + shift) % 26);
                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else result += c;
            }
            return result;
        }

        public static string Decrypt(string text, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty.");

            text = text.ToUpper();
            key = key.ToUpper();
            string result = string.Empty;
            int keyIndex = 0;

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    int shift = key[keyIndex] - 'A';
                    result += (char)('A' + (c - 'A' - shift + 26) % 26);
                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else result += c;
            }
            return result;
        }
    }
}
