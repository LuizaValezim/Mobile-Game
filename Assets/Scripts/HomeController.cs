using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HomeController : BaseController
{
    public override void Awake()
    {
        base.Awake();

    }

    private void OnDestroy()
    {
    }

    private void Start()
    {
    }

    public void onPlay()
    {
        Debug.Log("onPlay");
        // LevelManager.continueGamePlay();
    }

}
