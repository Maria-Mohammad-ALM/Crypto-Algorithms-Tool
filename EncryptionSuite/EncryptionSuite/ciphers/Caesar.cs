namespace EncryptionSuite.Ciphers
{
    public static class Caesar
    {
        public static string Encrypt(string text, int key)
        {
            key = ((key % 26) + 26);
            char[] buffer = text.ToUpper().ToCharArray();

            for (int i = 0; i < buffer.Length; i++)
                if (char.IsLetter(buffer[i]))
                {


                    buffer[i] = (char)(((buffer[i] - 'A' + key) % 26) + 'A');
                }

            return new string(buffer);
        }

        public static string Decrypt(string text, int key)
        {
            return Encrypt(text, 26 - (key % 26));
        }
    }
}
