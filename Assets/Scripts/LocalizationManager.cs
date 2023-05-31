using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager
{
    public static LocalizationManager Instance;

    public Action onLanguageChanged;

    private Dictionary<string, string> textLanguage = new Dictionary<string, string>();

    private string currentLan;

    private Dictionary<string, string> languageName = new Dictionary<string, string>();

    static LocalizationManager()
    {
        LocalizationManager.Instance = new LocalizationManager();
        LocalizationManager.Instance.languageName.Add("English", "ENGLISH");
        LocalizationManager.Instance.languageName.Add("Portuguese", "PORTUGUÊS");
        LocalizationManager.Instance.currentLan = PrefManager.getLocalizetion();
        if (LocalizationManager.Instance.currentLan == string.Empty)
        {
            LocalizationManager.Instance.currentLan = LocalizationManager.Instance.getDefaultLan();
        }
        LocalizationManager.Instance.loadLanguage();
    }

    public string getLanguageName()
    {
        Debug.Log("getLanguageName: " + this.languageName[this.currentLan]);
        return this.languageName[this.currentLan];
    }

    public string getLanguage()
    {
        return this.currentLan;
    }

    public void changeLanguage()
    {
        bool flag = false;
        string text = string.Empty;
        foreach (KeyValuePair<string, string> current in this.languageName)
        {
            if (text == string.Empty)
            {
                text = current.Key;
            }
            if (current.Key == this.currentLan)
            {
                flag = true;
            }
            else if (flag)
            {
                this.setLanguage(current.Key);
                return;
            }
        }
        this.setLanguage(text);
    }

    private string getDefaultLan()
    {
        string text = Application.systemLanguage.ToString();
        if (this.languageName.ContainsKey(text))
        {
            return text;
        }
        return "English";
    }

    private void setLanguage(string lan)
    {
        PrefManager.setLocalization(lan);
        if (lan != this.currentLan)
        {
            this.currentLan = lan;
            this.loadLanguage();
            if (this.onLanguageChanged != null)
            {
                this.onLanguageChanged();
            }
        }
    }

    private void loadLanguage()
    {
        this.textLanguage.Clear();
        string text = Resources.Load<TextAsset>("Localization/Text/" + this.currentLan).text;
        string[] array = text.Split(new char[]
        {
            '\r',
            '\n'
        }, StringSplitOptions.RemoveEmptyEntries);
        string[] array2 = array;
        for (int i = 0; i < array2.Length; i++)
        {
            string text2 = array2[i];
            string[] array3 = text2.Split(new char[]
            {
                '|'
            });
            array3[1] = array3[1].Replace('^', '\n');
            this.textLanguage.Add(array3[0], array3[1]);
        }
    }

    public string getLocalizeString(string key)
    {
        if (this.textLanguage.ContainsKey(key))
        {
            return this.textLanguage[key];
        }
        return string.Empty;
    }

    public string getLocalizeStringFormat(string key, object value)
    {
        if (this.textLanguage.ContainsKey(key))
        {
            return string.Format(this.textLanguage[key], value);
        }
        return string.Empty;
    }

    public Sprite getLocalizeSprite(string key)
    {
        return Resources.Load<Sprite>("Localization/Image/" + this.currentLan + "/" + key);
    }
}
