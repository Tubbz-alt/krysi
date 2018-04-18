using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPN.CTR.Framework
{
    public class Ctr
    {
        public string K { get; private set; }
        public int L { get; private set; }
        private Spn _spn;

        public Ctr(string k, int l, Spn spn)
        {
            K = k ?? throw new ArgumentNullException(nameof(k));
            L = l;
            _spn = spn ?? throw new ArgumentNullException(nameof(spn));
        }

        public string Encrypt(string text)
        {
            return String.Empty;
        }

        public IEnumerable<string> Decrypt(string text)
        {
            List<string> resultList = new List<string>();

            // Zerlege y in Blöcke der Länge L
            string[] parts = Helper.SplitInParts(text, L).ToArray();
            string yMinusOne = parts[0];
            resultList.Add(yMinusOne);

            for (int i = 1; i < parts.Length; i++)
            {
                // (y-1) + 0 + 1 + n-1
                int yResult = Convert.ToInt32((Convert.ToInt32(yMinusOne, 2) + (i - 1)) % Math.Pow(2, L));
                string binaryStringYResult = Helper.ConvertStringToGoodShape(Convert.ToString(yResult, 2));

                // Entschlüsselung vom binaryStringYResult mit dem SPN und dem Key
                string spnResult = _spn.Decrypt(binaryStringYResult);

                // XOR spnResult mit y0, y1, yn-1 values
                string xi = Helper.XorStrings(spnResult, parts[i]);
                resultList.Add(xi);
            }

            return resultList;
        }
    }
}
