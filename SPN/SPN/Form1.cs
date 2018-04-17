using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // SBox
            Dictionary<string, string> sbox = new Dictionary<string, string>()
            {
                { "0000", "1110" },
                { "0001", "0100" },
                { "0010", "1101" },
                { "0011", "0001" },
                { "0100", "0010" },
                { "0101", "1111" },
                { "0110", "1011" },
                { "0111", "1000" },
                { "1000", "0011" },
                { "1001", "1010" },
                { "1010", "0110" },
                { "1011", "1100" },
                { "1100", "0101" },
                { "1101", "1001" },
                { "1110", "0000" },
                { "1111", "0111" }
            };

            // Bitpermutation
            Dictionary<int, int> bitPermutation = new Dictionary<int, int>()
            {
                { 0, 0 },
                { 1, 4 },
                { 2, 8 },
                { 3, 12 },
                { 4, 1 },
                { 5, 5 },
                { 6, 9 },
                { 7, 13 },
                { 8, 2 },
                { 9, 6 },
                { 10, 10 },
                { 11, 14 },
                { 12, 3 },
                { 13, 7 },
                { 14, 11 },
                { 15, 15 }
            };

            //Spn mySpn = new Spn(4, 4, 4, 32, "00010001001010001000110000000000", 16, 4, sbox, bitPermutation);
            //mySpn.Encrypt("0001001010001111");

            // string[] stringsForCtr = Helper.PrepareStringCharsForCtr("fisch");
        }
    }
}