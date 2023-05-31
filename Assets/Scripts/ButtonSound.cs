using System;

public class ButtonSound : ButtonBase
{
    public override void OnClick()
    {
        SoundManager.Instance.playSound(SoundManager.Sound.button);
    }
}
