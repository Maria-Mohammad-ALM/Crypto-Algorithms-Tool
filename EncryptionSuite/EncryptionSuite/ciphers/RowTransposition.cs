using System;
using System.Linq;
using System.Text;

namespace EncryptionSuite.Ciphers
{
    public static class RowTransposition
    {
        public static string Encrypt(string text, string key)
        {
            text = CleanText(text);
            int[] order = key.Select(c => int.Parse(c.ToString())).ToArray();
            int cols = key.Length;
            int rows = (int)Math.Ceiling((double)text.Length / cols);

            char[,] table = new char[rows, cols];
            int index = 0;

            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    table[r, c] = index < text.Length ? text[index++] : 'X';

            StringBuilder result = new();
            foreach (int colNum in order.OrderBy(x => x))
            {
                int col = Array.IndexOf(order, colNum);
                for (int r = 0; r < rows; r++)
                    result.Append(table[r, col]);
            }

            return result.ToString();
        }

        public static string Decrypt(string text, string key)
        {
            int[] order = key.Select(c => int.Parse(c.ToString())).ToArray();
            int cols = key.Length;
            int rows = (int)Math.Ceiling((double)text.Length / cols);

            char[,] table = new char[rows, cols];
            int index = 0;

            foreach (int colNum in order.OrderBy(x => x))
            {
                int col = Array.IndexOf(order, colNum);
                for (int r = 0; r < rows; r++)
                    table[r, col] = text[index++];
            }

            StringBuilder result = new();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    result.Append(table[r, c]);

            return result.ToString().TrimEnd('X');
        }

        private static string CleanText(string text) =>
            new string(text.ToUpper().Where(char.IsLetter).ToArray());
    }
}
