using System;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
    }
}
