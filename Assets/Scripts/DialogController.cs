using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static DialogController instance;

    // [HideInInspector]
    public Dialog current;

    // [HideInInspector]
    public Dialog[] baseDialogs;

    public Action onDialogsOpened;

    public Action onDialogsClosed;

    public Stack<Dialog> dialogs = new Stack<Dialog>();

    public void Awake()
    {
        DialogController.instance = this;
    }

    public void ShowDialog(int type)
    {
        this.ShowDialog((DialogType)type, DialogShow.DONT_SHOW_IF_OTHERS_SHOWING, string.Empty);
    }

    public void ShowDialog(DialogType type, DialogShow option = DialogShow.REPLACE_CURRENT, string from = "")
    {
        Dialog dialog = this.GetDialog(type, from);
        this.ShowDialog(dialog, option);
    }

    public void ShowDialog(Dialog dialog, DialogShow option = DialogShow.REPLACE_CURRENT)
    {
        if (this.current != null)
        {
            if (option == DialogShow.DONT_SHOW_IF_OTHERS_SHOWING)
            {
                UnityEngine.Object.Destroy(dialog.gameObject);
                return;
            }
            if (option == DialogShow.REPLACE_CURRENT)
            {
                dialog.GetComponent<Canvas>().sortingOrder = this.current.GetComponent<Canvas>().sortingOrder;
                this.current.Close();
            }
            else if (option == DialogShow.STACK)
            {
                this.current.Hide();
            }
            else if (option == DialogShow.OVER_CURRENT)
            {
                int sortingOrder = this.current.GetComponent<Canvas>().sortingOrder;
                dialog.GetComponent<Canvas>().sortingOrder = sortingOrder + 20;
            }
        }
        this.current = dialog;
        if (option != DialogShow.SHOW_PREVIOUS)
        {
            Dialog expr_CE = this.current;
            expr_CE.onDialogOpened = (Action<Dialog>)Delegate.Combine(expr_CE.onDialogOpened, new Action<Dialog>(this.OnOneDialogOpened));
            Dialog expr_F5 = this.current;
            expr_F5.onDialogClosed = (Action<Dialog>)Delegate.Combine(expr_F5.onDialogClosed, new Action<Dialog>(this.OnOneDialogClosed));
            this.dialogs.Push(this.current);
        }
        this.current.Show();
        if (this.onDialogsOpened != null)
        {
            this.onDialogsOpened();
        }
    }

    public Dialog GetDialog(DialogType type, string from = "")
    {
        Dialog dialog = this.baseDialogs[(int)type];
        dialog.dialogType = type;
        dialog.from = from;
        return UnityEngine.Object.Instantiate<Dialog>(dialog, base.transform.position, base.transform.rotation);
    }

    public void CloseCurrentDialog()
    {
        if (this.current != null)
        {
            this.current.Close();
        }
    }

    public void CloseDialog(DialogType type)
    {
        if (this.current == null)
        {
            return;
        }
        if (this.current.dialogType == type)
        {
            this.current.Close();
        }
    }

    public bool IsDialogShowing()
    {
        return this.current != null;
    }

    public bool IsDialogShowing(DialogType type)
    {
        return !(this.current == null) && this.current.dialogType == type;
    }

    public bool isTopDialog(DialogType type)
    {
        return !(this.current == null) && this.current.dialogType == type;
    }

    private void OnOneDialogOpened(Dialog dialog)
    {
    }

    private void OnOneDialogClosed(Dialog dialog)
    {
        if (this.current == dialog)
        {
            this.current = null;
            this.dialogs.Pop();
            if (this.onDialogsClosed != null && this.dialogs.Count == 0)
            {
                this.onDialogsClosed();
            }
            if (this.dialogs.Count > 0)
            {
                if (dialog.dialogType == DialogType.Loading)
                {
                    this.current = this.dialogs.Peek();
                }
                else
                {
                    this.ShowDialog(this.dialogs.Peek(), DialogShow.SHOW_PREVIOUS);
                }
            }
        }
    }
}
