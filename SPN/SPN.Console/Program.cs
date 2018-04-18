using System;
using System.Collections.Generic;
using System.Linq;
using SPN.CTR.Framework;

namespace SPN.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //// SBox
            //Dictionary<string, string> sbox = new Dictionary<string, string>()
            //{
            //    { "0000", "1110" },
            //    { "0001", "0100" },
            //    { "0010", "1101" },
            //    { "0011", "0001" },
            //    { "0100", "0010" },
            //    { "0101", "1111" },
            //    { "0110", "1011" },
            //    { "0111", "1000" },
            //    { "1000", "0011" },
            //    { "1001", "1010" },
            //    { "1010", "0110" },
            //    { "1011", "1100" },
            //    { "1100", "0101" },
            //    { "1101", "1001" },
            //    { "1110", "0000" },
            //    { "1111", "0111" }
            //};

            //// Bitpermutation
            //Dictionary<int, int> bitPermutation = new Dictionary<int, int>()
            //{
            //    { 0, 0 },
            //    { 1, 4 },
            //    { 2, 8 },
            //    { 3, 12 },
            //    { 4, 1 },
            //    { 5, 5 },
            //    { 6, 9 },
            //    { 7, 13 },
            //    { 8, 2 },
            //    { 9, 6 },
            //    { 10, 10 },
            //    { 11, 14 },
            //    { 12, 3 },
            //    { 13, 7 },
            //    { 14, 11 },
            //    { 15, 15 }
            //};

            // SBox
            Dictionary<string, string> sbox = new Dictionary<string, string>()
            {
                { "0000", "0101" },
                { "0001", "0100" },
                { "0010", "1101" },
                { "0011", "0001" },
                { "0100", "0011" },
                { "0101", "1100" },
                { "0110", "1011" },
                { "0111", "1000" },
                { "1000", "1010" },
                { "1001", "0010" },
                { "1010", "0110" },
                { "1011", "1111" },
                { "1100", "1001" },
                { "1101", "1110" },
                { "1110", "0000" },
                { "1111", "0111" }
            };

            // Bitpermutation
            Dictionary<int, int> bitPermutation = new Dictionary<int, int>()
            {
                { 0, 4 },
                { 1, 5 },
                { 2, 8 },
                { 3, 9 },
                { 4, 0 },
                { 5, 1 },
                { 6, 10 },
                { 7, 11 },
                { 8, 2 },
                { 9, 3 },
                { 10, 6 },
                { 11, 7 }
            };

            // Key für Aufgabe
            //00010001001010001000110000000000

            Dictionary<string, string> sBoxInverse = sbox.ToDictionary(kp => kp.Value, kp => kp.Key);

            Spn mySpn = new Spn(3, 4, 3, 24, "000110101111110000000111", 4, sbox, bitPermutation);
            string chiffretext = mySpn.Encrypt("111101010110");
            string klartext = mySpn.Decrypt(chiffretext);


            string[] stringsForCtr = Helper.PrepareStringCharsForCtr("fisch");
        }
    }
}
