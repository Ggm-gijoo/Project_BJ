using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marker
{
    public class DrawMarker : MonoBehaviour
    {
        public GameObject linePrefab;
        public GameObject currentLine;

        private LineRenderer lineRenderer;
        private EdgeCollider2D edgeCollider2D;
        private List<Vector2> fingerPositions = new List<Vector2>();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            if(Input.GetMouseButton(0))
            {
                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //UpdateLine(tempFingerPos);
                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > 0.1f)
                {
                    UpdateLine(tempFingerPos);
                }
            }
        }

        private void CreateLine()
        {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            lineRenderer = currentLine.GetComponent<LineRenderer>();
            edgeCollider2D = currentLine.GetComponent<EdgeCollider2D>();
            fingerPositions.Clear();
            fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            fingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lineRenderer.SetPosition(0, fingerPositions[0]);
            lineRenderer.SetPosition(1, fingerPositions[1]);
            edgeCollider2D.points = fingerPositions.ToArray();
        }

        private void UpdateLine(Vector2 newFingerPos)
        {
            fingerPositions.Add(newFingerPos);
            lineRenderer.positionCount  = fingerPositions.Count;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
            edgeCollider2D.points = fingerPositions.ToArray();
        }
        
    }
}
