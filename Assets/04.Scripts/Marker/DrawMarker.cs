using Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marker
{
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
        
        public string lineaddress;
        public GameObject currentLine;

        private LineRenderer lineRenderer;
        private List<Vector2> fingerPositions = new List<Vector2>();
        private Vector3 mousePos;
        private BaseMarker currentMarker;
        private Camera inGameCam;

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
                CreateLine();
                currentMarker?.OnBeginDraw();
            }
            if(Input.GetMouseButton(0))
            {
                Vector2 tempFingerPos = inGameCam.ScreenToWorldPoint(mousePos);
                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                {
                    UpdateLine(tempFingerPos);
                    currentMarker?.OnDrawing();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentMarker?.OnEndDraw();
                currentMarker = null;
            }
        }

        private void CreateLine()
        {
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
    }
}
