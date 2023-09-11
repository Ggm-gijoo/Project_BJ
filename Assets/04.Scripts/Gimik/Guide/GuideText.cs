using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GuideText : MonoBehaviour
{
	[SerializeField] private TextMeshPro text;
	[SerializeField] private bool isFadeInWithStart;
	[SerializeField] private SpriteRenderer guideSpriteRenderer;

	public void Start()
	{
		text.alpha = 0f;
		if (guideSpriteRenderer != null)
		{
			Color color = guideSpriteRenderer.color;
			color.a = 0f;
			guideSpriteRenderer.color = color;
		}
		if (isFadeInWithStart)
		{
			FadeIn();
		}
	}

	public void FadeIn()
	{
		if(guideSpriteRenderer != null)
		{
			guideSpriteRenderer.DOFade(1f, 1f);
		}
		text.DOFade(1f, 1f);
	}
}
