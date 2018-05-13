using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainbowTable
{
    public class PasswordGenerator
    {
        public static readonly string ValidChars = "0123456789abcdefghijklmnopqrstuvwxyz";
        private List<string> _passwords = new List<string>();

        public List<string> GeneratePasswords(int amount)
        {
            return Dive("", 0, amount);
        }

        // https://stackoverflow.com/questions/3640174/generating-every-character-combination-up-to-a-certain-word-length
        private List<string> Dive(string prefix, int level, int passwordNumber)
        {
            if (_passwords.Count < passwordNumber)
            {
                level += 1;
                foreach (char c in ValidChars)
                {
                    string pw = prefix + c;
                    if (pw.Length == 7)
                    {
                        _passwords.Add(pw);
                    }

                    if (level < 8)
                    {
                        Dive(prefix + c, level, passwordNumber);
                    }
                }
            }

            return _passwords;
        }
    }
}
