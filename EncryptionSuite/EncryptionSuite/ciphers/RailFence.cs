using System;
using System.Text;
using System.Linq;

namespace EncryptionSuite.Ciphers
{
    public static class RailFence
    {
        public static string Encrypt(string text, int key)
        { 
            if (key <= 1) return text;
            text = CleanText(text);

            StringBuilder[] rails = Enumerable.Range(0, key).Select(_ => new StringBuilder()).ToArray();
            int dir = 1, row = 0;

            foreach (char c in text)
            {
                rails[row].Append(c); //سطرالتنفيذ الرئيسي
                row += dir;
                if (row == 0 || row == key - 1) dir *= -1;
            }

            StringBuilder result = new();
            foreach (var rail in rails) result.Append(rail);
            return result.ToString();
        }

        public static string Decrypt(string text, int key)
        {
            if (key <= 1) return text;
            int len = text.Length;

           
            bool[,] zigzag = new bool[len, key];
            int dir = 1, row = 0;
            for (int i = 0; i < len; i++)
            {
                zigzag[i, row] = true;
                row += dir;
                if (row == 0 || row == key - 1) dir *= -1;
            }

           
            char[,] grid = new char[len, key];
            int index = 0;
            for (int r = 0; r < key; r++)
                for (int i = 0; i < len; i++)
                    if (zigzag[i, r]) grid[i, r] = text[index++];

           
            StringBuilder result = new();
            dir = 1; row = 0;
            for (int i = 0; i < len; i++)
            {
                result.Append(grid[i, row]);
                row += dir;
                if (row == 0 || row == key - 1) dir *= -1;
            }

            return result.ToString();//سطر التنفيذ النهائي
        }

        private static string CleanText(string text) =>
            new string(text.ToUpper().Where(char.IsLetter).ToArray());
    }
}
