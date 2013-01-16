using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace GuiAccess
{
    public static class UserHash
    {
        //service interface
       //[DllImport("cypAce.dll", CharSet = CharSet.Unicode)]
       // public static extern string CypHashUserInfo(string name, string pw);
        public const char const_chHi = '$';
        public const char const_chLow = '&';
        public static string HashUserInfo(string name, string pw)
        {
            string strName = name.ToLower();

            return GetHarassString(name, pw, const_chHi, const_chLow);
        }

        public static string GetHarassString(string name, string pw, char chHi, char chLow)
        {
            int chFirst = (int)name[0];
            chFirst = chFirst & 0x0f;
            string strHarass = "";
	        for(int i=0;i<4;i++)
	        {
		        if((chFirst>>i&0x01) != 0 )
			        strHarass = chHi + strHarass;
		        else
			        strHarass = chLow + strHarass;
	        }
	        string strUserInfoHarassed = "";
	        strUserInfoHarassed += name;
	        strUserInfoHarassed += strHarass;
	        strUserInfoHarassed +=pw;

            SHA1Managed sh1 = new SHA1Managed();
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] txtBytes = uEncode.GetBytes(strUserInfoHarassed);
            byte[] hashedPassword = sh1.ComputeHash(txtBytes);
            string strHash = "";    
            foreach (byte oneByte in hashedPassword)
            {
                string strTmp;
                strTmp = string.Format("{0:x2}", oneByte);
                strHash += strTmp;
            }

            return strHash;
        }
        
    }
}
