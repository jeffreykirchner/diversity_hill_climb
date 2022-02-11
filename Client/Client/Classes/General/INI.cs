using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Client
{
    public static class INI
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        //functions for accessing INI files
        public static string getINI(string sFileName, string family, string key)
        {
            System.Text.StringBuilder strItem = new System.Text.StringBuilder(256);
            GetPrivateProfileString(family, key, "?", strItem, strItem.Capacity, sFileName);

            return strItem.ToString();
        }

        public static void writeINI(string sFileName, string family, string key, string value)
        {
            WritePrivateProfileString(family, key, value, sFileName);
        }
    }
}
