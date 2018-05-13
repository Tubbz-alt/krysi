using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RainbowTable.Extensions;

namespace RainbowTable
{
    public class RainbowTable
    {
        private static readonly int _chainLength = 2000;
        private static readonly int _chainRows = 2000;
        private static readonly int _passwordLength = 7;
        Dictionary<string, string> _startEndWords = new Dictionary<string, string>();

        /*Generate table for each _chainRows password, _chainLength hash values
        * @param [List<string>] generated password list
        */
        public void GenerateRainbowTable(List<string> passwords)
        {
            Parallel.For(_startEndWords.Count, _chainRows, (int i) =>
            {
                int j = 0;

                // Save start and end values of the chain to the tables - 3.24 Hashfunktionen
                string wordStart = passwords[i];
                string wordEnd = wordStart;

                //calculate each wordEnd
                while (j < _chainLength)
                {
                    wordEnd = GenerateEndWord(wordEnd, j);
                    j++;
                }

                //check if last hash (wordEnd) does not already exist in dictionary
                if (!_startEndWords.ContainsValue(wordEnd)) 
                {
                    _startEndWords.Add(wordStart, wordEnd);
                }
            });
        }

        /* Search a hashed 7 char password in rainbowtable
         * @param [String] searched hash value
         * @return [String] found endWord in dictionary
         */
        public string SearchHashPassword(string hashedPassword)
        {
            string result = string.Empty;

            for (int i = 1999; i >= 0; i--)
            {
                int l = 1999 - i;
                string wordwordToProcess = string.Empty;
                wordwordToProcess = Reduce(hashedPassword, i);

                //Hash and Reduce hashed Password (Loop through chain of hashed Password)
                for (int j = 0; j < l; j++)
                {
                    wordwordToProcess = EasyMD5.Hash(wordwordToProcess);
                    wordwordToProcess = Reduce(wordwordToProcess, (i + j) + 1);
                }

                // check if calculated word is equals a endWord value in the dictionary
                if (_startEndWords.ContainsValue(wordwordToProcess))
                {
                    Console.WriteLine($"Found match: {wordwordToProcess}");
                    string key = _startEndWords.FirstOrDefault(x => x.Value == wordwordToProcess).Key;
                    result = key;
                    break;
                }
            }

            return result;
        }
        /* Get plain passwort of hashed password
         * @param [String] an endword of the dictionary
         * @param [BigInteger] hash value of search password
         * @return [String] plain password
         */
        public string FindPlainText(string input, BigInteger targetHash)
        {
            string match = input;
            string hashedInput = EasyMD5.Hash(input);
            BigInteger hashedInputBigInt = BigInteger.Parse(hashedInput, NumberStyles.AllowHexSpecifier);
            int c = 0;

            //Loop through chain of the found endword from the startword
            //The last reduced word before targetHash is equals to a chain hash element is the plain password
            while (c < _chainLength && !hashedInputBigInt.Equals(targetHash))
            {
                match = Reduce(hashedInput, c++);
                hashedInput = EasyMD5.Hash(match);
                hashedInputBigInt = BigInteger.Parse(hashedInput, NumberStyles.AllowHexSpecifier);
            }

            return match;
        }

        /*Generates endword for the rainbowtable
        * @param [String] generated start password
        * @param [int] nte element of the chain
        * @return [String] possible endword
        */
        private string GenerateEndWord(string startWord, int level)
        {
            // First hash the string
            string hashedWord = EasyMD5.Hash(startWord);
            // Second apply reducing function
            return Reduce(hashedWord, level);
        }

        /*Reduction function
        * @param [String] hash value
        * @param [int] level of chain element
        * @return [String] reduced hash value
        */
        private string Reduce(string word, int level)
        {
            
            word = word.Insert(0, "0"); // Hack so that BigInt works correct in C#
            BigInteger intValue = BigInteger.Parse(word, NumberStyles.AllowHexSpecifier);
            intValue += level;
            string validChars = PasswordGenerator.ValidChars;

            StringBuilder sb = new StringBuilder();

            for (short i = 1; i <= _passwordLength; i++)
            {
                BigInteger div = intValue / validChars.Length;
                BigInteger mod = intValue % validChars.Length;
                intValue = div;
                sb.Append(validChars[(int)mod]);
            }

            return sb.ToString().Reverse();
        }
    }
}
