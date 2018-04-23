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

        /// <summary>
        /// Verschlüsselung von einem Text im Ctr Modus mit Hilfe eines SPN's
        /// </summary>
        /// <param name="text">Text, welcher verschlüsselt werden soll</param>
        /// <returns>Verschlüsselter Text als String</returns>
        public string Encrypt(string text)
        {
            // String hübsch machen, für CTR
            string ctrString = CtrHelper.PrepareStringForCtr(text, L);

            // Generierung von einem zufällen Bit-String der Länge L --> y[-1]
            String yMinusOne = CtrHelper.RandomNumberForCtrMode(L);

            // ctrString in Teile der Länge L aufteilen
            string[] parts = ctrString.SplitInParts(L).ToArray();

            // Erstellung Resultat-Liste und hinzufügen von y[n-1]
            List<string> resultList = new List<string>();
            resultList.Add(yMinusOne);

            // Ctr Steps
            for (int i = 0; i < parts.Length; i++)
            {
                // y[-1] + 0 + 1 + n-1
                int yResult = Convert.ToInt32((Convert.ToInt32(yMinusOne, 2) + i) % Math.Pow(2, L));
                string binaryStringYResult = Convert.ToString(yResult, 2).PadLeft(16, '0');

                // Verschlüsselungs vom binaryStringYResult mit dem SPN und dem Key
                string spnResult = _spn.Decrypt(binaryStringYResult, false);

                // XOR spnResult mit x0, x1, xn-1 values
                string xi = spnResult.Xor(parts[i]);
                resultList.Add(xi);
            }

            return string.Join(String.Empty, resultList);
        }

        /// <summary>
        /// Entschlüsselung von einem Text im Ctr Modus mit Hilfe eines SPN's
        /// </summary>
        /// <param name="text">Text, welcher entschlüsselt werden soll</param>
        /// <param name="isDecryptAfterEncrypt">Boolean welcher angibt, ob nach Verschlüsselung entschlüsselt wird</param>
        /// <returns>Entschlüsselter Text als String</returns>
        public string Decrypt(string text, bool isDecryptAfterEncrypt)
        {
            // Zerlege y in Blöcke der Länge L
            List<string> parts = text.SplitInParts(L).ToList();

            // Resultat-Liste erstellen
            List<string> resultList = new List<string>();

            // y[-1] holen
            string yMinusOne = parts[0];

            // Entferne y[n-1] von der Liste
            parts.RemoveAt(0);

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

            string result = string.Join(String.Empty, resultList);
            // Remove Padding from result Bitstring
            return result.Substring(0, result.LastIndexOf("1", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}