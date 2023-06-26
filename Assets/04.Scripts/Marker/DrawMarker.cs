using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marker
{
    public class DrawMarker : MonoBehaviour
    {
        public GameObject LinePrefab
        {
            get
            {
                return linePrefab;
            }
            set
            {
                linePrefab = value;
            }
        }
        
        public GameObject linePrefab;
        public GameObject currentLine;

        private LineRenderer lineRenderer;
        private List<Vector2> fingerPositions = new List<Vector2>();
        private Vector3 mousePos;
        private BaseMarker currentMarker;

        private void Update()
        {
            mousePos = Input.mousePosition;
            mousePos.z = 10;
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
                currentMarker?.OnBeginDraw();
            }
            if(Input.GetMouseButton(0))
            {
                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(mousePos);
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
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            currentMarker = currentLine.GetComponent<BaseMarker>();
            lineRenderer = currentLine.GetComponent<LineRenderer>();
            fingerPositions.Clear();
            fingerPositions.Add(Camera.main.ScreenToWorldPoint(mousePos));
            fingerPositions.Add(Camera.main.ScreenToWorldPoint(mousePos));
            lineRenderer.SetPosition(0, fingerPositions[0]);
            lineRenderer.SetPosition(1, fingerPositions[1]);
        }

        private void UpdateLine(Vector2 newFingerPos)
        {
            fingerPositions.Add(newFingerPos);
            lineRenderer.positionCount  = fingerPositions.Count;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        }
        
    }
}
