using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Marker;

public class MarkerImage : MonoBehaviour
{

	[SerializeField]
	private MarkerType markerType;

	[SerializeField]
	private GameObject markerObj;
	[SerializeField]
	private Outline outLine;
	[SerializeField]
	private Image fillColorImage;


	public void UpdateUseMarkerUI()
	{
		var drawMaker = MarkerManager.Instance.drawMarker;
		float value = 0f;
		switch (markerType)
		{

			case MarkerType.None:
				value = 0f;
				break;
			case MarkerType.Black:
				value = drawMaker.BlackGauge / drawMaker.BlackMaxGauge;
				break;
			case MarkerType.Gravity:
				value = drawMaker.GravityGauge / drawMaker.GravityMaxGauge;
				break;
			case MarkerType.Rubber:
				value = drawMaker.RubberGauge / drawMaker.RubberMaxGauge;
				break;
		}
		Vector2 newPos = fillColorImage.rectTransform.anchoredPosition;
		newPos.y = Mathf.Lerp(-164f, -92.6f, value);
		fillColorImage.rectTransform.anchoredPosition = newPos;
	}

	public void GetItem()
	{
		if(markerType == MarkerType.Black && InventoryManager.Instance.inventoryData.isGetBlackMarker)
		{
			gameObject.SetActive(true);
			UpdateUseMarkerUI();
		}
		else if (markerType == MarkerType.Rubber && InventoryManager.Instance.inventoryData.isGetRubberMarker)
		{
			gameObject.SetActive(true);
			UpdateUseMarkerUI();
		}
		else if (markerType == MarkerType.Gravity && InventoryManager.Instance.inventoryData.isGetGravityMarker)
		{
			gameObject.SetActive(true);
			UpdateUseMarkerUI();
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

}
