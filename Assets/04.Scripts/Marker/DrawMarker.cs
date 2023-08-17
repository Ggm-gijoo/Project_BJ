using Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marker
{
    public enum MarkerType
    {
        None,
        Black,
        Gravity,
        Rubber
    }

    public class DrawMarker : MonoBehaviour
    {
        public string LineAddress
        {
            get
            {
                return lineaddress;
            }
            set
            {
                lineaddress = value;
            }
        }
        public MarkerType MarkerType
        {
            get
            {
                return markerType;
            }
            set
            {
                markerType = value;
            }
        }

		public string lineaddress;
        public GameObject currentLine;

        [SerializeField] private LayerMask cantTouchLayerMask;
        private LineRenderer lineRenderer;
        private List<Vector2> fingerPositions = new List<Vector2>();
        private Vector3 mousePos;
        private BaseMarker currentMarker;
        private Camera inGameCam;
		private MarkerType markerType;


		private float blackGauge = 10f;
		[SerializeField] private float blackMaxGauge = 10f;
		private float gravityGauge = 10f;
		[SerializeField] private float gravityMaxGauge = 10f;
		private float rubberGauge = 10f;
		[SerializeField] private float rubberMaxGauge = 10f;

        private float praviouseGauge;

		private void Update()
        {
            if (inGameCam == null) 
            {
                return;
            }
            mousePos = Input.mousePosition;
            mousePos.z = 10;
			if (Input.GetMouseButtonDown(0))
			{
				praviouseGauge = GetCurrentGauge();
				CreateLine();
                currentMarker?.OnBeginDraw();
			}

			if (Input.GetMouseButton(0))
			{
				Vector2 tempFingerPos = inGameCam.ScreenToWorldPoint(mousePos);
				float _distance = Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]);
				if (_distance > 0.1f)
				{
					//面倒 贸府
					if(CheckMousePosGround(tempFingerPos) || CheckBothPointBetween(fingerPositions[fingerPositions.Count - 1], tempFingerPos))
					{
						return;
					}
					//付目 侩樊 眉农
					if (GetCurrentGauge() <= 0f)
					{
						return;
					}
					Debug.Log(_distance);
					UpdateLine(tempFingerPos);
                    currentMarker?.OnDrawing();
                    RemoveGauge(markerType, _distance);
				}
            }

            if (Input.GetMouseButtonUp(0))
            {
                if(currentMarker != null)
				{
					currentMarker.Gauge = praviouseGauge - blackGauge;
					currentMarker.OnEndDraw();
					currentMarker = null;
				}
            }
        }

        private void CreateLine()
		{
			if (GetCurrentGauge() <= 0f)
			{
				return;
			}
			currentLine = ObjectPoolManager.Instance.GetObject(lineaddress, Vector3.zero, Quaternion.identity);
            currentMarker = currentLine.GetComponent<BaseMarker>();
            lineRenderer = currentMarker.LineRenderer;
            fingerPositions.Clear();
            fingerPositions.Add(inGameCam.ScreenToWorldPoint(mousePos));
            fingerPositions.Add(inGameCam.ScreenToWorldPoint(mousePos));
            lineRenderer.positionCount = 2;
			lineRenderer.SetPosition(0, fingerPositions[0]);
            lineRenderer.SetPosition(1, fingerPositions[1]);
        }

        private void UpdateLine(Vector2 newFingerPos)
        {
            fingerPositions.Add(newFingerPos);
            lineRenderer.positionCount  = fingerPositions.Count;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        }
        
        public void ResetCamera()
        {
            inGameCam = InGameCam.instance.GetComponent<Camera>();
		}

        public void ResetGauge()
		{
			blackGauge = blackMaxGauge;
			gravityGauge = gravityMaxGauge;
			rubberGauge = rubberMaxGauge;
		}

        public void AddGauge(MarkerType markerType, float gauge)
        {
            switch(markerType)
			{
				default:
				case MarkerType.Black:
                    blackGauge += gauge;
                    if(blackGauge > blackMaxGauge)
                    {
                        blackGauge = blackMaxGauge;
                    }
				break;
				case MarkerType.Gravity:
					gravityGauge += gauge;
					if (gravityGauge > gravityMaxGauge)
					{
						gravityGauge = gravityMaxGauge;
					}
					break;
				case MarkerType.Rubber:
					rubberGauge += gauge;
					if (rubberGauge > rubberMaxGauge)
					{
						rubberGauge = rubberMaxGauge;
					}
					break;
			}
        }

        private float GetCurrentGauge()
        {
            switch(markerType)
            {
                default:
                case MarkerType.Black:
                    return blackGauge;
				case MarkerType.Gravity:
					return gravityGauge;
				case MarkerType.Rubber:
					return rubberGauge;
			}
        }

        private void RemoveGauge(MarkerType markerType, float remove)
		{
			switch (markerType)
			{
				default:
				case MarkerType.Black:
					blackGauge -= remove;
					if (blackGauge < 0f)
					{
						blackGauge = 0f;
					}
					break;
				case MarkerType.Gravity:
					gravityGauge -= remove;
					if (gravityGauge < 0f)
					{
						gravityGauge = 0f;
					}
					break;
				case MarkerType.Rubber:
					rubberGauge -= remove;
					if (rubberGauge < 0f)
					{
						rubberGauge = 0f;
					}
					break;
			}
		}

        private bool CheckMousePosGround(Vector3 pos)
		{
			
			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, cantTouchLayerMask);

			if (hit.collider != null)
			{
				return true;
			}
			else
			{
				return false;
			}
        }

		private bool CheckBothPointBetween(Vector3 startPoint, Vector3 endPoint)
		{
			Vector2 direction = endPoint - startPoint;
			RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, direction.magnitude, cantTouchLayerMask);

			if (hit.collider != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
    }
}
