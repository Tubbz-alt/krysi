using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SPN
{
    class Spn
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
        private Dictionary<int, string> _roundKeys;

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
            _roundKeys = new Dictionary<int, string>();

            // Calculate Round Keys
            CalculateRoundKeys();
        }

        private void CalculateRoundKeys()
        {
            for (int i = 0; i <= R; i++)
            {
                _roundKeys.Add(i, Key.Substring((RoundKeyPosition * i), RoundKeyLength));
            }

            Console.WriteLine("Runden-Schlüssel:");
            foreach (var roundKey in _roundKeys)
            {
                Console.WriteLine(roundKey);
            }
        }

        public void Encrypt(string textToEncrypt)
        {
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
        }
    }
}
