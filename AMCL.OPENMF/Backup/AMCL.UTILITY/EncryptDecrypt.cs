using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.IO;

/// <summary>
/// Summary description for EncryptDecrypt
/// </summary>
namespace AMCL.UTILITY
{
    public class EncryptDecrypt
    {
        public EncryptDecrypt()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string Encrypt(string str)
        {
            string encr = null;
            if (str == "")
            {
                encr = "ÄÆ8¼";
                return encr;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char ch = Convert.ToChar(str.Substring(i, 1));
                    int num = ch + 169;
                    char enchar = (char)num;
                    encr = encr + enchar.ToString();
                }
            }

            return encr;
        }
        public string Decrypt(string str)
        {
            string decr = null;
            if (str == "ÄÆ8¼")
            {
                decr = "";
                return decr;
            }
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char ch = Convert.ToChar(str.Substring(i, 1));
                    int num = ch - 169;
                    char dechar = (char)num;
                    decr = decr + dechar.ToString();
                }
            }
            return decr;
        }
        public string PhotoBase64ImgSrc(string fileNameandPath)
        {
            byte[] byteArray = File.ReadAllBytes(fileNameandPath);
            string base64 = Convert.ToBase64String(byteArray);

            return string.Format("data:image/jpg;base64,{0}", base64);
        }

    }
}
