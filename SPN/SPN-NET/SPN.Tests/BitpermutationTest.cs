using System;
using System.Collections.Generic;
using System.Text;
using SPN.CTR.Framework.ExtensionMethods;
using Xunit;

namespace SPN.Tests
{
    public class BitpermutationTest : IDisposable
    {
        private Dictionary<int, int> _bitPermutation;
        private string _textToPermute;

        public BitpermutationTest()
        {
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

            _textToPermute = "000001110010";

        }

        [Fact]
        public void BitpermutationOnStringTest()
        {
            Assert.Equal("010000100011", _textToPermute.Bitpermutation(_bitPermutation));
        }

        public void Dispose()
        {
            _bitPermutation = null;
            _textToPermute = default(string);
        }
    }
}
