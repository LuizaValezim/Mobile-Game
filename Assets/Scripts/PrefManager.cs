using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefManager
{
    public static void setBgMusiceEnable(bool on)
    {
        PlayerPrefs.SetInt("BgMusiceEnable", (!on) ? 0 : 1);
    }

    public static bool isBgMusiceEnable()
    {
        return PlayerPrefs.GetInt("BgMusiceEnable", 1) == 1;
    }

    public static void setSoundEnable(bool enable)
    {
        PlayerPrefs.SetInt("SoundEnable", (!enable) ? 0 : 1);
    }

    public static bool isSoundEnable()
    {
        return PlayerPrefs.GetInt("SoundEnable", 1) != 0;
    }

    public static void setLocalization(string lan)
    {
        PlayerPrefs.SetString("Localization_lan", lan);
    }

    public static string getLocalizetion()
    {
        return PlayerPrefs.GetString("Localization_lan", string.Empty);
    }

}
