using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Commons.Helpers
{
    public static class Crypto
    {
        /// <summary>
        /// Usado para convertir un texto a hash MD5
        /// </summary>
        /// <param name="word">Texto a convertir</param>
        /// <returns>Devuelve un string MD5</returns>
        public static string HashMD5(string word)
        {
            string hashString = string.Empty;
            using (MD5 mD5 = MD5.Create())
            {
                byte[] bytes = Encoding.Unicode.GetBytes(word);
                var hash = mD5.ComputeHash(bytes);

                foreach (byte x in hash)
                {
                    hashString += String.Format("{0:x2}", x);
                }
            }
            return hashString;
        }
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Base64ToString(string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }
    }
}
