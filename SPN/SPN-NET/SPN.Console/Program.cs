﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using SPN.CTR.Framework;
using SPN.CTR.Framework.ExtensionMethods;

namespace SPN.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // SBox
            Dictionary<string, string> sbox = new Dictionary<string, string>()
            {
                { "0000", "1110" },
                { "0001", "0100" },
                { "0010", "1101" },
                { "0011", "0001" },
                { "0100", "0010" },
                { "0101", "1111" },
                { "0110", "1011" },
                { "0111", "1000" },
                { "1000", "0011" },
                { "1001", "1010" },
                { "1010", "0110" },
                { "1011", "1100" },
                { "1100", "0101" },
                { "1101", "1001" },
                { "1110", "0000" },
                { "1111", "0111" }
            };

            // Bitpermutations-Tabelle
            Dictionary<int, int> bitPermutation = new Dictionary<int, int>()
            {
                { 0, 0 },
                { 1, 4 },
                { 2, 8 },
                { 3, 12 },
                { 4, 1 },
                { 5, 5 },
                { 6, 9 },
                { 7, 13 },
                { 8, 2 },
                { 9, 6 },
                { 10, 10 },
                { 11, 14 },
                { 12, 3 },
                { 13, 7 },
                { 14, 11 },
                { 15, 15 }
            };

            Spn mySpn = new Spn(4, 4, 4, 32, "00111010100101001101011000111111", 4, sbox, bitPermutation);
            Ctr ctr = new Ctr("00111010100101001101011000111111", 16, mySpn);
            string klartext = ctr.Decrypt(@"00000100110100100000101110111000000000101000111110001110011111110110000001010001010000111010000000010011011001110010101110110000", false);

            System.Console.WriteLine("-- Klartext als BitString --");
            System.Console.WriteLine(klartext);

            var data = klartext.GetBytesFromBinaryString();
            var text = Encoding.ASCII.GetString(data);

            System.Console.WriteLine("-- Klartext als Text --");
            System.Console.WriteLine(text);

            System.Console.ReadLine();
        }
    }
}
