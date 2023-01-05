using System;
using UnityEngine;

namespace Gamee.Hiuk.Adapter 
{
    public static class PlayerPrefsAdapter
    {
        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        public static int GetInt(string key, int valueDefaut = 0)
        {
            return PlayerPrefs.GetInt(key, valueDefaut);
        }

        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        public static float GetFloat(string key, float valueDefaut = 0)
        {
            return PlayerPrefs.GetFloat(key, valueDefaut);
        }

        public static void SetBool(string key, bool value)
        {
            var compileValue = value == false ? 0 : 1;
            PlayerPrefs.SetInt(key, compileValue);
        }
        public static bool GetBool(string key, bool valueDefaut = false)
        {
            var compileValue = valueDefaut == false ? 0 : 1;
            return PlayerPrefs.GetInt(key, compileValue) == 0 ? false : true;
        }

        public static void SetDateTime(string key, DateTime value)
        {
            PlayerPrefs.GetString(key, value.ToString());
        }

        public static DateTime GetDateTime(string key, string valueDefaut = "01/01/0001 00:00:00")
        {
            var compileValue = DateTime.Parse(PlayerPrefs.GetString(key, valueDefaut));
            return compileValue;
        }
    }
}

