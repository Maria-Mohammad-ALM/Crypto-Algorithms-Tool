using System;
using System.Linq;
using System.Text;

namespace EncryptionSuite.Ciphers
{
    public static class Feistel
    {
        public static string Encrypt(string text, string key) =>
            Process(text, key, true);

        public static string Decrypt(string text, string key) =>
            Process(text, key, false);

        private static string Process(string text, string key, bool encrypt)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Text and key cannot be empty.");

            // تنظيف النص والمفتاح
            byte[] data = Encoding.UTF8.GetBytes(CleanText(text));
            byte[] keyBytes = Encoding.UTF8.GetBytes(CleanKey(key));

            // نتأكد أن طول البيانات زوجي (نعالج حالة طول فردي)
            if (data.Length % 2 != 0)
                data = data.Concat(new[] { (byte)'X' }).ToArray();

            byte[] result = new byte[data.Length];
            int rounds = 4;

            if (encrypt)
            {
                for (int i = 0; i < data.Length; i += 2)
                {
                    byte left = data[i];
                    byte right = data[i + 1];

                    for (int round = 0; round < rounds; round++)
                    {
                        byte f = F(right, keyBytes[round % keyBytes.Length]);
                        (left, right) = (right, (byte)(left ^ f));
                    }

                    result[i] = left;
                    result[i + 1] = right;
                }

                return Convert.ToBase64String(result);
            }
            else
            {
                // عند فك التشفير نتوقع Base64 من المستخدم
                byte[] cipherBytes;
                try
                {
                    cipherBytes = Convert.FromBase64String(text);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Cipher text is not valid Base64. Use the string returned by Encrypt.");
                }

                if (cipherBytes.Length % 2 != 0)
                    throw new ArgumentException("Invalid cipher length.");

                for (int i = 0; i < cipherBytes.Length; i += 2)
                {
                    byte left = cipherBytes[i];
                    byte right = cipherBytes[i + 1];

                    for (int round = rounds - 1; round >= 0; round--)
                    {
                        byte f = F(left, keyBytes[round % keyBytes.Length]);
                        (left, right) = ((byte)(right ^ f), left);
                    }

                    result[i] = left;
                    result[i + 1] = right;
                }

                // نزيل أي بايتات padding التي أضفناها (الحرف 'X' الذي أضفناه لو كان طول فردي)
                var plain = Encoding.UTF8.GetString(result);
                return plain.TrimEnd('\0').TrimEnd('X');
            }
        }

        // الدالة الأساسية (يمكن تغييرها لاحقاً لتدعم AND/OR/XOR)
        private static byte F(byte half, byte key) => (byte)(half ^ key);

        // تنظيف النص: نحذف الفراغات والرموز الغير مهمة ونبقي الحروف والأرقام
        private static string CleanText(string text)
        {
            var clean = new string(text.Where(c => !char.IsWhiteSpace(c)).ToArray());
            // نترك الحروف كما هي (لا نحذف علامات التشكيل). نضيف X إذا كان الطول فردي لاحقًا.
            return clean;
        }

        // تنظيف المفتاح: نحتفظ بالحروف والأرقام ونضمن على الأقل بايت واحد
        private static string CleanKey(string key)
        {
            var k = new string(key.Where(char.IsLetterOrDigit).ToArray());
            if (string.IsNullOrEmpty(k)) k = "K";
            return k;
        }
    }
}
