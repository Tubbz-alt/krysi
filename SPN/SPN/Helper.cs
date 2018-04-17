using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPN
{
    static class Helper
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

        public static string WordSubstitution(string s, Dictionary<string, string> sBox)
        {
            string[] stringParts = SplitInParts(s, 4).ToArray();

            for (int i = 0; i < stringParts.Length; i++)
            {
                stringParts[i] = sBox[stringParts[i]];
            }

            return MakeFlatString(stringParts);
        }

        public static string Bitpermutation(string s, Dictionary<int, int> bitpermutation)
        {
            string copyOriginal = string.Copy(s);
            var resultBuilder = new StringBuilder(s);

            for (int i = 0; i < s.Length; i++)
            {
                char value = copyOriginal[i];
                int pos = bitpermutation[i];
                resultBuilder.Remove(pos, 1);
                resultBuilder.Insert(pos, s[i]);
            }

            return resultBuilder.ToString();
        }

        public static string[] PrepareStringCharsForCtr(string value)
        {
            // Convert the string into a byte[].
            byte[] bytearray = Encoding.ASCII.GetBytes(value);
            string[] result = new string[bytearray.Length];

            for (int i = 0; i < bytearray.Length; i++)
            {
                string binaryString = Convert.ToString(Convert.ToInt32(bytearray[i]), 2);
                string insert = binaryString.Insert(0, "1");

                while ((insert.Length) % 16 != 0)
                {
                    insert = insert.Insert(0, "0");
                }

                result[i] = insert;
            }

            return result;
        }

        private static IEnumerable<string> SplitInParts(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        private static string MakeFlatString(string[] stringArray)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < stringArray.Length; i++)
            {
                sb.Append(stringArray[i]);
            }

            return sb.ToString();
        }
    }
}