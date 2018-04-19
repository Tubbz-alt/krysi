using System;
using System.Collections.Generic;
using System.Text;
using SPN.CTR.Framework.ExtensionMethods;
using Xunit;

namespace SPN.Tests
{
    public class SBoxTest
    {
        private Dictionary<string, string> _sbox;
        private string _textToSbox;

        public SBoxTest()
        {
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

            _textToSbox = "111011111001";

        }

        [Fact]
        public void SBoxOnStringTest()
        {
            Assert.Equal("000001110010", _textToSbox.WordSubstitution(_sbox));
        }

        public void Dispose()
        {
            _sbox = null;
            _textToSbox = default(string);
        }
    }
}
