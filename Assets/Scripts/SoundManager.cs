using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        button,
        popup,
    }

    public static SoundManager Instance;

    private AudioSource audioSource;

    private AudioSource bgAudioSource;

    private Dictionary<SoundManager.Sound, AudioClip> soundCache = new Dictionary<SoundManager.Sound, AudioClip>();

    private AudioClip bg_start;

    private AudioClip bg_gameplay;

    static SoundManager()
    {
        GameObject gameObject = new GameObject("SoundManager");
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
        SoundManager.Instance = gameObject.AddComponent<SoundManager>();
        SoundManager.Instance.audioSource = gameObject.AddComponent<AudioSource>();
        SoundManager.Instance.bgAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void playSound(SoundManager.Sound sound)
    {
        if (!PrefManager.isSoundEnable())
        {
            return;
        }
        if (!this.soundCache.ContainsKey(sound))
        {
            this.soundCache.Add(sound, Resources.Load("Audio/" + sound.ToString()) as AudioClip);
        }
        this.audioSource.PlayOneShot(this.soundCache[sound]);
    }

    public void playBg()
    {
        AudioClip bgClip = this.getBgClip();
        if (this.bgAudioSource.isPlaying && this.bgAudioSource.clip == bgClip)
        {
            return;
        }
        this.bgAudioSource.Stop();
        this.bgAudioSource.clip = this.getBgClip();
        this.bgAudioSource.loop = true;
        this.bgAudioSource.Play();
    }

    public void stopBg()
    {
        this.bgAudioSource.Stop();
    }

    public void bgPause()
    {
        this.bgAudioSource.Pause();
    }

    public void bgResume()
    {
        this.bgAudioSource.UnPause();
    }

    private AudioClip getBgClip()
    {
        if (this.bg_gameplay == null)
        {
            this.bg_gameplay = (Resources.Load("Audio/bg_gameplay") as AudioClip);
        }
        return this.bg_gameplay;
    }
}
