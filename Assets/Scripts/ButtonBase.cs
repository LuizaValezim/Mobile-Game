using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
	public virtual void Start()
	{
		Button component = base.GetComponent<Button>();
		if (component != null)
		{
			component.onClick.AddListener(new UnityAction(this.OnClick));
		}
	}

	public virtual void OnClick()
	{
	}
}
