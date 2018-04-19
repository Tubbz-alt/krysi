using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPN.CTR.Framework.ExtensionMethods;
using SPN.CTR.Framework.Helper;

namespace SPN.CTR.Framework
{
    public class Ctr
    {
        // Properties
        public string K { get; private set; }
        public int L { get; private set; }
        private Spn _spn;

        // Ctor
        public Ctr(string k, int l, Spn spn)
        {
            K = k ?? throw new ArgumentNullException(nameof(k));
            L = l;
            _spn = spn ?? throw new ArgumentNullException(nameof(spn));
        }

        public string Encrypt(string text)
        {
            string stringArray = CtrHelper.PrepareStringForCtr(text, L);

            // For testing purposes, use "0000010011010010".
            String yMinusOne = CtrHelper.RandomNumberForCtrMode(16);

            // Ausgangs-String in Teile aufteilen
            string[] parts = stringArray.SplitInParts(L).ToArray();
            List<string> resultList = new List<string>();
            resultList.Add(yMinusOne);

            for (int i = 0; i < parts.Length; i++)
            {
                // (y-1) + 0 + 1 + n-1
                int yResult = Convert.ToInt32((Convert.ToInt32(yMinusOne, 2) + i) % Math.Pow(2, L));
                string binaryStringYResult = Convert.ToString(yResult, 2).PadLeft(16, '0');

                // Entschlüsselung vom binaryStringYResult mit dem SPN und dem Key
                string spnResult = _spn.Decrypt(binaryStringYResult, false);

                // XOR spnResult mit y0, y1, yn-1 values
                string xi = spnResult.Xor(parts[i]);
                resultList.Add(xi);
            }

            return string.Join(String.Empty, resultList);
        }

        public string Decrypt(string text, bool isDecryptAfterEncrypt)
        {
            List<string> resultList = new List<string>();

            // Zerlege y in Blöcke der Länge L
            List<string> parts = text.SplitInParts(L).ToList();
            string yMinusOne = parts[0];

            // Entferne y[n-1] von der Liste
            parts.RemoveAt(0);

            //resultList.Add(yMinusOne);

            for (int i = 0; i < parts.Count; i++)
            {
                // (y-1) + 0 + 1 + n-1
                int yResult = Convert.ToInt32((Convert.ToInt32(yMinusOne, 2) + i) % Math.Pow(2, L));
                string binaryStringYResult = Convert.ToString(yResult, 2).PadLeft(16, '0');

                // Entschlüsselung vom binaryStringYResult mit dem SPN und dem Key
                string spnResult = _spn.Decrypt(binaryStringYResult, isDecryptAfterEncrypt);

                // XOR spnResult mit y0, y1, yn-1 values
                string xi = spnResult.Xor(parts[i]);
                resultList.Add(xi);
            }

            return string.Join(String.Empty, resultList);
        }
    }
}
