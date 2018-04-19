using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPN.CTR.Framework.ExtensionMethods
{
    public static class CustomExtensions
    {
        #region String Extensions

        /// <summary>
        /// Extension Methode für den Typen String.
        /// Berechnet auf Basis eines Binary-Strings die Bytes
        /// </summary>
        /// <param name="binaryString">Binary-String</param>
        /// <returns>Byte-Array mit Bytes aus Binary-String</returns>
        public static Byte[] GetBytesFromBinaryString(this string binaryString)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binaryString.Length; i += 8)
            {
                String t = binaryString.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }

        /// <summary>
        /// Extension Methode für den Typen String.
        /// Mit dieser Methode ist es möglich, 2 Strings direkt zu XOREN. Die XOR-Operation wird dabei auf den einzelnen Character angewendet.
        /// </summary>
        /// <param name="a">1 String</param>
        /// <param name="b">2 String</param>
        /// <returns>Die Methode gibt das Resultat der XOR-Operation als String zurück</returns>
        public static string Xor(this string a, string b)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < a.Length; i++)
            {
                sb.Append((Convert.ToInt32(a[i]) ^ Convert.ToInt32(b[i])).ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Extension Methode für den Typen String.
        /// Führt eine Wortsubstitution von einem String mit einer mitgegebenen sBox durch
        /// </summary>
        /// <param name="text">String, welcher Wort-substituiert werden soll</param>
        /// <param name="sBox">SBox, wird für Substitution benötigt</param>
        /// <returns></returns>
        public static string WordSubstitution(this string text, Dictionary<string, string> sBox)
        {
            string[] stringParts = SplitInParts(text, 4).ToArray();

            for (int i = 0; i < stringParts.Length; i++)
            {
                stringParts[i] = sBox[stringParts[i]];
            }

            return string.Join(String.Empty, stringParts);
        }

        /// <summary>
        /// Extension Methode für den Typen String.
        /// Führt eine Bitpermutation für einen String mit einer mitgegebenen Bit-Box aus
        /// </summary>
        /// <param name="text">String, welcher permutiert werden soll</param>
        /// <param name="bitpermutation">Bit-Box, welche die Positionen für die Bit-Permutation beinhaltet</param>
        /// <returns></returns>
        public static string Bitpermutation(this string text, Dictionary<int, int> bitpermutation)
        {
            var resultBuilder = new StringBuilder(text);

            for (int i = 0; i < text.Length; i++)
            {
                int position = bitpermutation[i];
                resultBuilder.Remove(position, 1);
                resultBuilder.Insert(position, text[i]);
            }

            return resultBuilder.ToString();
        }

        /// <summary>
        /// Extension Methode für den Typen String.
        /// Teilt einen String in Blöcke mit gleicher Länge auf.
        /// Die Länge wird dabei der Methode mitgegeben
        /// </summary>
        /// <param name="text">String, welcher gesplittet werden soll</param>
        /// <param name="chunkSize">Grösse der Blöcke</param>
        /// <returns>IEnumerable von einem String-Type</returns>
        public static IEnumerable<string> SplitInParts(this string text, int chunkSize)
        {
            return Enumerable.Range(0, text.Length / chunkSize).Select(i => text.Substring(i * chunkSize, chunkSize));
        }

        #endregion
    }
}
