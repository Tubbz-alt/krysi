using System;
using System.Collections.Generic;
using System.Text;
using SPN.CTR.Framework.ExtensionMethods;
using Xunit;

namespace SPN.Tests
{
    public class XorTest
    {
        private string _textOne;
        private string _textTwo;

        public XorTest()
        {
            _textOne = "111011111001";
            _textTwo = "000110100111";

        }

        [Fact]
        public void SBoxOnStringTest()
        {
            Assert.Equal("111101011110", _textOne.Xor(_textTwo));
        }

        public void Dispose()
        {
            _textOne = null;
            _textTwo = null;
        }
    }
}
