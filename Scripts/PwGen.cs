using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW_Manager.Scripts
{
    public class PwGen
    {

        public String GeneratePassword(bool upper = true, bool lower = true, bool numbers = true, bool symbols = true, int lenght = 24)
        {
            string pw = "";
            string stringlist = "";

            if (upper) stringlist += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (lower) stringlist += "abcdefghiklmnopqrstuvwxyz";
            if (numbers) stringlist += "0123456789";
            if (symbols) stringlist += @"!?#$%^&*()_+-=/<>,.|\`~'" + "\"";

            char[] charlist = stringlist.ToCharArray();

            for(int i = 0; i < lenght; i++)
            {
                Random rnd = new Random();
                pw += charlist[rnd.Next(charlist.Length)];
            }


            return pw;
        }
    }
}
