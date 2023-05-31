using System;
using UnityEngine;

public class UIAdapter : MonoBehaviour
{
	private void Awake()
	{

		if (this.neetCutBottom())
		{
			RectTransform component2 = base.GetComponent<RectTransform>();
			component2.offsetMin = new Vector2(component2.offsetMin.x, 170f);
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private bool neetCutBottom()
	{
		return false;
	}


}
