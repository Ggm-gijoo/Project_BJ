using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Marker;

namespace UI
{
	public class MarkerGaugeUI : MonoBehaviour
	{
		[SerializeField]
		private GameObject markerObj;
		[SerializeField]
		private Outline outLine;
		[SerializeField]
		private Image fillColorImage;

		[SerializeField]
		private MarkerImage markerImageBlack;
		[SerializeField]
		private MarkerImage markerImageGravity;
		[SerializeField]
		private MarkerImage markerImageRubber;

		public void UpdateUseMarkerUI()
		{
			var drawMaker = MarkerManager.Instance.drawMarker;
			float value = 0f;
			switch (DrawMarker.Instance.MarkerType)
			{

				case MarkerType.None:
					value = 0f;
					break;
				case MarkerType.Black:
					value = drawMaker.BlackGauge / drawMaker.BlackMaxGauge;
					markerImageBlack.UpdateUseMarkerUI();
					break;
				case MarkerType.Gravity:
					value = drawMaker.GravityGauge / drawMaker.GravityMaxGauge;
					markerImageGravity.UpdateUseMarkerUI();
					break;
				case MarkerType.Rubber:
					value = drawMaker.RubberGauge / drawMaker.RubberMaxGauge;
					markerImageRubber.UpdateUseMarkerUI();
					break;
			}
			Vector2 newPos = fillColorImage.rectTransform.anchoredPosition;
			newPos.y = Mathf.Lerp(-222f, -34f, value);
			fillColorImage.rectTransform.anchoredPosition = newPos;
		}

		public void UpdateChangeMarkerUI()
		{
			var drawMaker = MarkerManager.Instance.drawMarker;
			switch (drawMaker.MarkerType)
			{

				case MarkerType.None:
					markerObj.SetActive(false);
					break;
				case MarkerType.Black:
					markerObj.SetActive(true);
					outLine.effectColor = new Color(0f, 0f, 0f);
					fillColorImage.color = new Color(0.17f, 0.17f, 0.17f);
					break;
				case MarkerType.Gravity:
					markerObj.SetActive(true);
					outLine.effectColor = new Color(0.7f, 0.17f, 0.17f);
					fillColorImage.color = new Color(0.4f, 0f, 0f);
					break;
				case MarkerType.Rubber:
					markerObj.SetActive(true);
					outLine.effectColor = new Color(0f, 0.54f, 0.62f);
					fillColorImage.color = new Color(0f, 0.35f, 0.4f);
					break;
			}
		}
	}
}
