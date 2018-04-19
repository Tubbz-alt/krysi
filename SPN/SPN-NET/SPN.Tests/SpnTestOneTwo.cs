using System;
using System.Collections.Generic;
using SPN.CTR.Framework;
using Xunit;

namespace SPN.Tests
{
    public class SpnTestTwo : IDisposable
    {
        private Spn spn;
        private int _r;
        private int _n;
        private int _m;
        private int _s;
        private string _key;
        private int _roundKeyPosition;
        private Dictionary<string, string> _sbox;
        private Dictionary<int, int> _bitPermutation;
        private string _plainText;
        private string _chiffretext;

        public SpnTestTwo()
        {
            _m = 4;
            _n = 4;
            _r = 4;
            _s = 32;
            _key = "00010001001010001000110000000000";
            _roundKeyPosition = 4;
            _plainText = "0001001010001111";
            _chiffretext = "1010111010110100";

            _sbox = new Dictionary<string, string>()
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

            _bitPermutation = new Dictionary<int, int>()
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

            spn = new Spn(_r, _n, _m, _s, _key, _roundKeyPosition, _sbox, _bitPermutation);
        }

        [Fact]
        public void SpnWithDataFromSpnTaskEncryptTest()
        {
            Assert.Equal(_chiffretext, spn.Encrypt(_plainText));
        }

        [Fact]
        public void SpnWithDataFromSpnTaskDecryptTest()
        {
            Assert.Equal(_plainText, spn.Decrypt(spn.Encrypt(_plainText), true));
        }

        public void Dispose()
        {
            spn = null;
            _r = default(int);
            _n = default(int);
            _m = default(int);
            _s = default(int);
            _key = default(string);
            _roundKeyPosition = default(int);
            _sbox = null;
            _bitPermutation = null;
            _plainText = default(string);
            _chiffretext = default(string);
        }
    }
}
