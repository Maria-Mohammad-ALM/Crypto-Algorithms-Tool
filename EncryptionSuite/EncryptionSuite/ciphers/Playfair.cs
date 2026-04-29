using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EncryptionSuite.Ciphers
{
    public static class Playfair
    {
        public static string Encrypt(string text, string key) =>
            Process(text, key, true);

        public static string Decrypt(string text, string key) =>
            Process(text, key, false);

        private static string Process(string text, string key, bool encrypt)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Text and key cannot be empty.");

            BuildTable(key, out char[,] table, out Dictionary<char, (int r, int c)> pos);

            text = new string(text.ToUpper().Replace("J", "I").Where(char.IsLetter).ToArray());

            var pairs = new List<(char, char)>();
            for (int i = 0; i < text.Length; i += 2)
            {
                char a = text[i];
                char b = (i + 1 < text.Length) ? text[i + 1] : 'X';
                if (a == b) { b = 'X'; i--; }
                pairs.Add((a, b));
            }

            int dir = encrypt ? 1 : -1;
            var result = new StringBuilder();

            foreach (var (a, b) in pairs)
            {
                var (ra, ca) = pos[a];
                var (rb, cb) = pos[b];

                if (ra == rb)
                {
                    result.Append(table[ra, (ca + dir + 5) % 5]);
                    result.Append(table[rb, (cb + dir + 5) % 5]);
                }
                else if (ca == cb)
                {
                    result.Append(table[(ra + dir + 5) % 5, ca]);
                    result.Append(table[(rb + dir + 5) % 5, cb]);
                }
                else
                {
                    result.Append(table[ra, cb]);
                    result.Append(table[rb, ca]);
                }
            }

            return result.ToString();
        }

        private static void BuildTable(string key, out char[,] table, out Dictionary<char, (int r, int c)> pos)
        {
            table = new char[5, 5];
            pos = new Dictionary<char, (int r, int c)>();

            string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; // بدون J
            key = new string(key.ToUpper().Where(c => c >= 'A' && c <= 'Z').ToArray()).Replace("J", "I");

            var combined = new string((key + alphabet).Distinct().ToArray());

            int index = 0;
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                    table[r, c] = combined[index++];
            
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                    pos[table[r, c]] = (r, c);
        }
    }
}