using System;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public GameObject DontDestroy;

    private static GameObject dontDestroyInstance;

    public bool enableSound = true;

    public virtual void Awake()
    {
        if (BaseController.dontDestroyInstance == null)
        {
            BaseController.dontDestroyInstance = UnityEngine.Object.Instantiate<GameObject>(this.DontDestroy);
        }
        if (this.enableSound)
        {
            if (PrefManager.isBgMusiceEnable())
            {
                SoundManager.Instance.playBg();
            }
            else
            {
                SoundManager.Instance.stopBg();
            }
        }
    }
}
