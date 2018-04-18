using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SPN.CTR.Framework
{
    public class Spn : IMode
    {
        public int R { get; private set; }
        public int N { get; private set; }
        public int M { get; private set; }
        public int S { get; private set; }
        public string Key { get; private set; }
        public int RoundKeyLength { get; private set; }
        public int RoundKeyPosition { get; private set; }
        public Dictionary<string, string> SBox { get; private set; }
        public Dictionary<int, int> Bitpermutation { get; private set; }
        private string[] _roundKeys;
        private string[] _roundKeysInverse;

        public Spn(int r, int n, int m, int s, string key, int roundKeyLength, int roundKeyPosition,
            Dictionary<string, string> sBox, Dictionary<int, int> bitpermutation)
        {
            R = r;
            N = n;
            M = m;
            S = s;
            Key = key ?? throw new ArgumentNullException(nameof(key));
            RoundKeyLength = roundKeyLength;
            RoundKeyPosition = roundKeyPosition;
            SBox = sBox ?? throw new ArgumentNullException(nameof(sBox));
            Bitpermutation = bitpermutation;

            // Initialisiere Objekte
            _roundKeys = new string[R + 1];
            _roundKeysInverse = new string[R + 1];
        }

        public string Encrypt(string textToEncrypt)
        {
            // Calculate Round Keys
            CalculateRoundKeys();

            // Initial Step
            string x = Helper.XorStrings(textToEncrypt, _roundKeys[0]);

            // Normal SPN Steps
            for (int i = 1; i < R; i++)
            {
                // Substitution
                x = Helper.WordSubstitution(x, SBox);
                // Bitpermutation
                x = Helper.Bitpermutation(x, Bitpermutation);
                // Rundenschlüsseladdition
                x = Helper.XorStrings(x, _roundKeys[i]);
            }

            // Short Step
            x = Helper.WordSubstitution(x, SBox);
            x = Helper.XorStrings(x, _roundKeys[R]);

            return x;
        }

        public string Decrypt(string text)
        {
            // Berechnung der initialen Rundenschlüssel
            CalculateRoundKeys();

            // Berechnung der inversen Rundenschlüssel
            CalculateRoundKeysInverse();


            return String.Empty;
        }

        /// <summary>
        /// Berechnet die initialen Rundenschlüssel gemäss SPN und Definition der Rundenschlüsselfunktion
        /// </summary>
        private void CalculateRoundKeys()
        {
            _roundKeys = new string[R + 1];

            for (int i = 0; i <= R; i++)
            {
                _roundKeys[i] = Key.Substring((RoundKeyPosition * i), RoundKeyLength);
            }
        }

        /// <summary>
        /// Berechnet die Rundenschlüssel für die SPN Entschlüsselung
        /// Dabei wird der 0te und der Rte Rundenschlüssel einfach an der Position vertauscht
        /// Alle restlichen Rundenschlüssel tauschen auch die Position (von aussen nach innen), werden jedoch noch Bitpermutiert
        /// </summary>
        private void CalculateRoundKeysInverse()
        {
            for (int i = 0; i < _roundKeys.Length; i++)
            {
                if (i != 0 && i != _roundKeys.Length - 1)
                {
                    _roundKeysInverse[i] = Helper.Bitpermutation(_roundKeys[_roundKeys.Length - (i + 1)], Bitpermutation);
                }
                else
                {
                    _roundKeysInverse[i] = _roundKeys[_roundKeys.Length - (i+1)];
                }
            }
        }
    }
}
