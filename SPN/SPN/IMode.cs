using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPN
{
    public interface IMode
    {
        string Encrypt(string text);
        string Decrypt(string text);
    }
}
