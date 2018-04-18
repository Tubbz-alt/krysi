using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPN.CTR.Framework
{
    /// <summary>
    /// Interface stellt Encrypt und Decrypt Methoden zur Verfügung, welche mittels String aufgerufen werden können.
    /// Verschiedene Algorithmen sollen dieses Interface implementieren für einheitliche Schnittstelle
    /// </summary>
    public interface IMode
    {
        string Encrypt(string text);
        string Decrypt(string text);
    }
}
