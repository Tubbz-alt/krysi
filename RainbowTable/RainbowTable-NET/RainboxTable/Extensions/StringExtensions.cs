using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainbowTable.Extensions
{
    public static class StringExtensions
    {
        public static string Reverse(this string stringToReverse)
        {
            char[] charArray = stringToReverse.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
