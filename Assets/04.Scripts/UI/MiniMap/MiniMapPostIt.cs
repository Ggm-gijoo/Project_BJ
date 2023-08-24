using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using TMPro;

public class MiniMapPostIt : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer spriteRenderer;
	[SerializeField]
	private TextMeshPro textpro;

	public void SetMapDataSO(MapDataSO mapDataSO)
	{
		Sprite tex = Resources.Load<Sprite>($"MapDatas/Icon/{mapDataSO.iconType.ToString()}");
		spriteRenderer.sprite = tex;
		textpro.text = mapDataSO.explanation;
	}
}
