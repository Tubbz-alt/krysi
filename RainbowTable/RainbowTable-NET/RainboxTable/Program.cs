using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RainbowTable
{
    public class Program
    {
        private static readonly string hash = "1d56a37fb6b08aa709fe90e12ca59e12";

        static void Main(string[] args)
        {
            // Generate all passwords
            Console.WriteLine("Generating Passwords...");
            PasswordGenerator pwGenerator = new PasswordGenerator();
            List<String> passwords = pwGenerator.GeneratePasswords(2000);
            passwords.RemoveRange(2000, passwords.Count - 2000);
            Console.WriteLine("Passwords generated...");

            //Generate Rainbow object and create passwort table
            Console.WriteLine("Generating RainbowTable...");
            RainbowTable rt = new RainbowTable();
            rt.GenerateRainbowTable(passwords);
            Console.WriteLine("RainbowTable generated...");

            //Find searched hash in rainbow table
            Console.WriteLine("Search Hash Password...");
            string password = rt.SearchHashPassword(hash);
            Console.WriteLine($"HashPassword found: {password}");

            //Get plain text passwort of hash input
            Console.WriteLine("Search Plaintext...");
            string plainText = rt.FindPlainText(password, BigInteger.Parse(hash, NumberStyles.AllowHexSpecifier));
            Console.WriteLine($"Plaintext: {plainText}");
            Console.ReadKey();
        }
    }
}
