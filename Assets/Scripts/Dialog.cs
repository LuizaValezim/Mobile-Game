using System;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [HideInInspector]
    public Animator anim;

    public AnimationClip hidingAnimation;

    public Action<Dialog> onDialogOpened;

    public Action<Dialog> onDialogClosed;

    public Action onDialogCompleteClosed;

    public Action<Dialog> onButtonCloseClicked;

    public DialogType dialogType;

    public bool closeOnBack = true;

    public string from;

    private AnimatorStateInfo info;

    private bool showedAnim;

    private float TimeScalePre;

    private bool showCalled;

    protected virtual void Awake()
    {
        if (this.anim == null)
        {
            this.anim = base.GetComponent<Animator>();
        }
    }

    protected virtual void Start()
    {
        this.onDialogCompleteClosed = (Action)Delegate.Combine(this.onDialogCompleteClosed, new Action(this.OnDialogCompleteClosed));
        base.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    protected virtual void Update()
    {
        if (this.closeOnBack && UnityEngine.Input.GetKeyDown(KeyCode.Escape) && DialogController.instance.isTopDialog(this.dialogType))
        {
            this.OnBackPressed();
        }
    }

    public virtual void Show()
    {
        if (!this.showCalled && this.dialogType != DialogType.Loading)
        {
            SoundManager.Instance.playSound(SoundManager.Sound.popup);
        }
        base.gameObject.SetActive(true);
        if (this.anim != null && this.IsIdle() && !this.showedAnim)
        {
            this.showedAnim = true;
            this.anim.SetTrigger("show");
            this.onDialogOpened(this);
        }
        if (!this.showCalled)
        {
            this.showCalled = true;
            this.TimeScalePre = Time.timeScale;
            Time.timeScale = 0f;
        }
    }

    public virtual void OnBackPressed()
    {
        this.Close();
    }

    public virtual void Close()
    {
        Time.timeScale = this.TimeScalePre;
        if (this.anim != null && this.IsIdle() && this.hidingAnimation != null)
        {
            this.anim.SetTrigger("hide");
        }
        else
        {
            this.DoClose();
        }
        if (this.onDialogClosed != null)
        {
            this.onDialogClosed(this);
        }
    }

    public virtual void DoClose()
    {
        UnityEngine.Object.Destroy(base.gameObject);
        if (this.onDialogCompleteClosed != null)
        {
            this.onDialogCompleteClosed();
        }
    }

    public void Hide()
    {
        base.gameObject.SetActive(false);
    }

    public bool IsIdle()
    {
        this.info = this.anim.GetCurrentAnimatorStateInfo(0);
        return this.info.IsName("Idle");
    }

    public virtual void OnDialogCompleteClosed()
    {
        this.onDialogCompleteClosed = (Action)Delegate.Remove(this.onDialogCompleteClosed, new Action(this.OnDialogCompleteClosed));
    }
}
