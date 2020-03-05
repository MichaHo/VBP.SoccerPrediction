using System;
using System.Security.Cryptography;
using System.Text;

namespace SoccerPrediction.Helper
{
    public static class PasswordHelper
    {
        /// <summary>
        /// erstellt einen Hash aus einem übergebenen string
        /// </summary>
        /// <param name="plainText">der string im Klartext</param>
        /// <returns></returns>
        public static string GetHash(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            using (SHA512 hash = SHA512.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                return BitConverter.ToString(bytes);
            }
        }
    }
}
