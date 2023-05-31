using System;
using UnityEngine;
using UnityEngine.UI;
public class ButtonOpenDialog : ButtonBase
{
    public DialogType type;

    public DialogShow showType = DialogShow.REPLACE_CURRENT;

    public string from = string.Empty;

    public override void OnClick()
    {
        DialogController.instance.ShowDialog(this.type, this.showType, this.from);
    }
}
