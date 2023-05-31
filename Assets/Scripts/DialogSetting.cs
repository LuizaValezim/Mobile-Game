using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogSetting : Dialog
{
    public Image sound;

    public Image music;

    public Sprite soundOn;

    public Sprite soundOff;

    public Sprite musicOn;

    public Sprite musicOff;

    public GameObject removeAds;

    public Text language;

    private int testModeCount;

    protected override void Start()
    {
        base.Start();
        this.sound.overrideSprite = ((!PrefManager.isSoundEnable()) ? this.soundOff : this.soundOn);
        this.music.overrideSprite = ((!PrefManager.isBgMusiceEnable()) ? this.musicOff : this.musicOn);
        this.language.text = LocalizationManager.Instance.getLanguageName();
        if (ChannelController.channel == 0)
        {
            this.removeAds.SetActive(true);
        }
        else if (ChannelController.channel == 1)
        {
            this.removeAds.SetActive(false);
        }
    }

    public void onSound()
    {
        PrefManager.setSoundEnable(!PrefManager.isSoundEnable());
        this.sound.overrideSprite = ((!PrefManager.isSoundEnable()) ? this.soundOff : this.soundOn);
    }

    public void onMusic()
    {
        PrefManager.setBgMusiceEnable(!PrefManager.isBgMusiceEnable());
        this.music.overrideSprite = ((!PrefManager.isBgMusiceEnable()) ? this.musicOff : this.musicOn);
        if (PrefManager.isBgMusiceEnable())
        {
            SoundManager.Instance.playBg();
        }
        else
        {
            SoundManager.Instance.stopBg();
        }
    }

    public void onChangeLanguage()
    {
        LocalizationManager.Instance.changeLanguage();
        this.language.text = LocalizationManager.Instance.getLanguageName();
    }
}
