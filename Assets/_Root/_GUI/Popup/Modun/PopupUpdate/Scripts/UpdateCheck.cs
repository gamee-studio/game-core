using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Popup.Update 
{
    public static class UpdateCheck
    {
        public static bool CheckVersion(string strVersionNewUpdateValue)
        {
            int versionCurrentValue = ConvertVersionValue(Application.version);
            int versionNewUpdateValue = ConvertVersionValue(strVersionNewUpdateValue);
            if (versionNewUpdateValue > versionCurrentValue)
            {
                return true;
            }
            return false;
        }

        public static int ConvertVersionValue(string strVersion) 
        {
            string convert = strVersion.Replace(".", "");
            return int.Parse(convert);
        }
    }
}