using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPN.CTR.Framework
{
    public static class Helper
    {
        /// <summary>
        /// Mit dieser Methode ist es möglich, 2 Strings direkt zu XOREN. Die XOR-Operation wird dabei auf den einzelnen Character angewendet.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Die Methode gibt das Resultat der XOR-Operation als String zurück</returns>
        public static string XorStrings(string a, string b)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < a.Length; i++)
            {
                sb.Append((Convert.ToInt32(a[i]) ^ Convert.ToInt32(b[i])).ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Führt eine Wortsubstitution von einem String mit einer mitgegebenen sBox durch
        /// </summary>
        /// <param name="text">String, welcher Wort-substituiert werden soll</param>
        /// <param name="sBox">SBox, wird für Substitution benötigt</param>
        /// <returns></returns>
        public static string WordSubstitution(string text, Dictionary<string, string> sBox)
        {
            string[] stringParts = SplitInParts(text, 4).ToArray();

            for (int i = 0; i < stringParts.Length; i++)
            {
                stringParts[i] = sBox[stringParts[i]];
            }

            return ConvertStringArrayToString(stringParts);
        }

        /// <summary>
        /// Führt eine Bitpermutation für einen String mit einer mitgegebenen Bit-Box aus
        /// </summary>
        /// <param name="text">String, welcher permutiert werden soll</param>
        /// <param name="bitpermutation">Bit-Box, welche die Positionen für die Bit-Permutation beinhaltet</param>
        /// <returns></returns>
        public static string Bitpermutation(string text, Dictionary<int, int> bitpermutation)
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

        public static string PrepareStringForCtr(string value, int length)
        {
            if (value.Length % length != 0)
            {
                value = value.Insert(0, "1");
            }

            while (value.Length % length != 0)
            {
                value = value.Insert(0, "0");
            }

            return value;
        }

        /// <summary>
        /// Teilt einen String in Blöcke mit gleicher Länge auf.
        /// Die Länge wird dabei der Methode mitgegeben
        /// </summary>
        /// <param name="text">String, welcher gesplittet werden soll</param>
        /// <param name="chunkSize">Grösse der Blöcke</param>
        /// <returns>IEnumerable von einem String-Type</returns>
        public static IEnumerable<string> SplitInParts(string text, int chunkSize)
        {
            return Enumerable.Range(0, text.Length / chunkSize).Select(i => text.Substring(i * chunkSize, chunkSize));
        }

        /// <summary>
        /// Konvertiert String-Array zu einem "flachen" String
        /// Result-String enthält keine Trennzeichen oder Whitespaces
        /// </summary>
        /// <param name="stringArray">String-Array, welches konveritert werden soll</param>
        /// <returns>String</returns>
        public static string ConvertStringArrayToString(string[] stringArray)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < stringArray.Length; i++)
            {
                sb.Append(stringArray[i]);
            }

            return sb.ToString();
        }

        public static string ConvertStringToGoodShape(string text)
        {
            text = text.Insert(0, "1");
            while (text.Length % 16 != 0)
            {
                text = text.Insert(0, "0");
            }

            return text;
        }

        public static string RemovePadding(this string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(posA + 1);
        }

        public static Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }

        public static string RandomY(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random r = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(r.Next(2));
            }

            return sb.ToString();
        }
    }
}