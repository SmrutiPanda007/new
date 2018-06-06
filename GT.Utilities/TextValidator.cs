using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GT.Utilities
{
    public static class TextValidator
    {
        public static string Validate(string Data, string DataMode)
        {
            string Resp = "OK";
            string PlainText = "~___`___!___@___#___$___%___^___&___*___(___)___<___>___?___:___;___/___-___[___]___{___}___|___=___+____";
            string Password = "~___`___!___@___#___$___%___^___&___*___(___)___<___>___?___:___;___/___-___[___]___{___}___|___=___+____";
            string Email = "~___`___!___#___$___%___^___&___*___(___)___<___>___?___:___;___/___-___[___]___{___}___|___=___+,____";
            string Url = "";
            string CheckRe = "";

            if (DataMode == "PLAINTEXT")
            {
                CheckRe = PlainText;
            }
            else if (DataMode == "PASSWORD")
            {
                CheckRe = Password;
                return "OK";
            }
            else if (DataMode == "EMAIL")
            {
                CheckRe = Email;
            }
            else if (DataMode == "URL")
            {
                CheckRe = Url;
            }
            else
            {
                CheckRe = PlainText;

            }
            string[] delimiters = new string[] { "___" };
            foreach (var TChar in CheckRe.Split(delimiters, StringSplitOptions.None))
            {
                if (Data.Contains(TChar))
                {
                    return "Invalid Characters Found";
                }
            }
            return Resp;
        }

      
    }
}
