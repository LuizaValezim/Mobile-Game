using System;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour
{
    public string key;

    private void Awake()
    {
        LocalizationManager expr_05 = LocalizationManager.Instance;
        expr_05.onLanguageChanged = (Action)Delegate.Combine(expr_05.onLanguageChanged, new Action(this.setLocalizeText));
        this.setLocalizeText();
    }

    private void OnDestroy()
    {
        LocalizationManager expr_05 = LocalizationManager.Instance;
        expr_05.onLanguageChanged = (Action)Delegate.Remove(expr_05.onLanguageChanged, new Action(this.setLocalizeText));
    }

    private void Start()
    {
    }

    private void setLocalizeText()
    {
        Text component = base.GetComponent<Text>();
        if (component != null)
        {
            component.text = LocalizationManager.Instance.getLocalizeString(this.key);
        }
    }
}
