using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GuideText : MonoBehaviour
{
	[SerializeField] private TextMeshPro text;
	[SerializeField] private bool isFadeInWithStart;

	public void Start()
	{
		text.alpha = 0f;
		if(isFadeInWithStart)
		{
			FadeIn();
		}
	}

	public void FadeIn()
	{
		text.DOFade(1f, 1f);
	}
}
