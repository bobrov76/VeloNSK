using System;
using System.Security.Cryptography;
using System.Text;

namespace API.HelpClass
{
    public class PasswordGenerate
    {

        public string Get_Password_Two_Autentification(string passwd)
        {
            return Convert.ToBase64String(SHA512.Create().ComputeHash(Encoding.UTF8.GetBytes(passwd)));
        }
        public string Get_Passwd_Small()
        {
            string value = "";
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                value += rnd.Next(0, 9).ToString();
            }
            return value;
        }
        public string GetPassword()
        {
            string pass = "";
            //генератор поролей
            var r = new Random();
            while (pass.Length < 9)
            {
                Char c = (char)r.Next(33, 125);
                if (Char.IsLetterOrDigit(c))
                    pass += c;
            }
            return pass.ToString();
        }
    }    
}
