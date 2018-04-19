using System;
using System.Collections.Generic;
using SPN.CTR.Framework;
using Xunit;

namespace SPN.Tests
{
    public class SpnOneTest : IDisposable
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

        public SpnOneTest()
        {
            _m = 3;
            _n = 4;
            _r = 3;
            _s = 24;
            _key = "000110101111110000000111";
            _roundKeyPosition = 4;
            _plainText = "111101010110";
            _chiffretext = "000101111010";

            _sbox = new Dictionary<string, string>()
            {
                {"0000", "0101"},
                {"0001", "0100"},
                {"0010", "1101"},
                {"0011", "0001"},
                {"0100", "0011"},
                {"0101", "1100"},
                {"0110", "1011"},
                {"0111", "1000"},
                {"1000", "1010"},
                {"1001", "0010"},
                {"1010", "0110"},
                {"1011", "1111"},
                {"1100", "1001"},
                {"1101", "1110"},
                {"1110", "0000"},
                {"1111", "0111"}
            };

            _bitPermutation = new Dictionary<int, int>()
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

            spn = new Spn(_r, _n, _m, _s, _key, _roundKeyPosition, _sbox, _bitPermutation);
        }

        [Fact]
        public void SpnWithDataFromLectureOneEncryptTest()
        {
            Assert.Equal(_chiffretext, spn.Encrypt(_plainText));
        }

        [Fact]
        public void SpnWithDataFromLectureOneDecryptTest()
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
