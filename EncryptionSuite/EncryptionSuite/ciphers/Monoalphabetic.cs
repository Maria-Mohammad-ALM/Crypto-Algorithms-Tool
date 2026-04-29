using System;
using System.Collections.Generic;
using System.Linq;

namespace EncryptionSuite.Ciphers
{
    public static class Monoalphabetic
    {
        public static string Encrypt(string text, string key)
        {
            key = key.ToUpper();

            if (key.Length != 26 || key.Distinct().Count() != 26)
                throw new ArgumentException("Key must contain 26 unique letters.");

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Dictionary<char, char> map = new();
            for (int i = 0; i < 26; i++)
                map[alphabet[i]] = key[i];

            char[] buffer = text.ToUpper().ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
                if (char.IsLetter(buffer[i]))
                    buffer[i] = map[buffer[i]];

            return new string(buffer);
        }

        public static string Decrypt(string text, string key)
        {
            key = key.ToUpper();

            if (key.Length != 26 || key.Distinct().Count() != 26)
                throw new ArgumentException("Key must contain 26 unique letters.");

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Dictionary<char, char> map = new();
            for (int i = 0; i < 26; i++)
                map[key[i]] = alphabet[i];

            char[] buffer = text.ToUpper().ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
                if (char.IsLetter(buffer[i]))
                    buffer[i] = map[buffer[i]];

            return new string(buffer);
        }
    }
}
