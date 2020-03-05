using System;
using System.Runtime.InteropServices;
using System.Security;
namespace SoccerPrediction.Helper
{
    /// <summary>
    /// Helfer für die Klasse <see cref = "SecureString" />
    /// </ summary>
    public static class SecureStringHelper
    {
        /// <summary>
        /// hebt die Sicherung von <see cref = "SecureString" /> für einfachen Text auf
        /// </ summary>
        /// <param name = "secureString"> die sichere Zeichenfolge </ param>
        /// <returns> </ returns>
        public static string Unsecure(this SecureString secureString)
        {
            if (secureString == null) return string.Empty;

            var unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        /// <summary>
        /// Konvertiert eine ungesicherte Zeichenfolge in eine gesicherte Zeichenfolge.
        /// </ summary>
        /// <param name = "plainPassword"> das einfache Passwort </ param>
        public static SecureString Secure(this string plainPassword)
        {
            if (string.IsNullOrEmpty(plainPassword)) return new SecureString();

            SecureString secure = new SecureString();
            foreach (var ch in plainPassword)
            {
                secure.AppendChar(ch);
            }
            return secure;
        }

        /// <summary>
        /// Kopiert die vorhandene Instanz einer sicheren Zeichenfolge in das Ziel und löscht das Ziel zuvor
        /// </ summary>
        /// <param name = "source"> die sichere Zeichenfolge </ param>
        /// <param name = "destination"> das Ziel </ param>
        public static void CopyTo(this SecureString source, SecureString destination)
        {
            destination.Clear();
            foreach (var ch in source.Unsecure())
            {
                destination.AppendChar(ch);
            }
        }
    }
}
