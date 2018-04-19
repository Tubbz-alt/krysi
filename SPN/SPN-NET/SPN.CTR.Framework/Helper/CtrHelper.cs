using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPN.CTR.Framework.Helper
{
    public static class CtrHelper
    {
        /// <summary>
        /// Bereitet einen String für die Arbeit im CTR Modus vor.
        /// Dabei wird sichergestellt, dass der String durch die mitgegebene Länge teilbar ist.
        /// Falls dies initial nicht der Fall ist wird ein Padding hinzugefügt (zuerst eine 1, dann 0en), bis der String durch die Länge teilbar ist
        /// </summary>
        /// <param name="text">String, welcher vorbereitet wird</param>
        /// <param name="length">Länge, durch welche der String teilbar sein soll</param>
        /// <returns>CTR konformer String</returns>
        public static string PrepareStringForCtr(string text, int length)
        {
            // Falls nicht durch Länge teilbar, 1 einfügen
            if (text.Length % length != 0)
            {
                text = text.Insert(0, "1");
            }

            // Solange nicht durch 1 teilbar, 0 einfügen
            while (text.Length % length != 0)
            {
                text = text.Insert(0, "0");
            }

            return text;
        }

        /// <summary>
        /// Generiert einen random Bit-String mit der mitgegebenen Länge
        /// </summary>
        /// <param name="length">Länge für den random Bit-String</param>
        /// <returns>Random Bit-String</returns>
        public static string RandomNumberForCtrMode(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(random.Next(2));
            }

            return sb.ToString();
        }
    }
}