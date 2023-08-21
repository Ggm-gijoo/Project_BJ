using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marker
{
    public class GravityMarker : BaseMarker
    {
        [SerializeField]
        private Rigidbody2D rigid;
		[SerializeField]
		private LineRenderer lineRenderer;
		[SerializeField]
		private float multiply = 2f;
		[SerializeField]
		private GameObject trailRenderer;
		
		public override string Key
		{
			get
			{
				return "GravityMarker";
			}
		}

		public override void OnEndDraw()
        {
            base.OnEndDraw();

			rigid.isKinematic = false;
			float _mass = CalculateLineLength(lineRenderer);
			rigid.mass = _mass * _mass; //(CalculateLineDensity(lineRenderer) + CalculateLineLength(lineRenderer)) * multiply;
			trailRenderer.transform.position = CalculateShapeCenter(lineRenderer);
		}

		private float CalculateLineDensity(LineRenderer lineRenderer)
		{
			float lineLength = CalculateLineLength(lineRenderer);
			int pointCount = lineRenderer.positionCount;
			float density = pointCount / lineLength;

			return density;
		}

		private float CalculateLineLength(LineRenderer lineRenderer)
		{
			float length = 0f;
			int pointCount = lineRenderer.positionCount;

			for (int i = 0; i < pointCount - 1; i++)
			{
				Vector3 point1 = lineRenderer.GetPosition(i);
				Vector3 point2 = lineRenderer.GetPosition(i + 1);
				length += Vector3.Distance(point1, point2);
			}

			return length;
		}

		private Vector3 CalculateShapeCenter(LineRenderer lineRenderer)
		{
			int pointCount = lineRenderer.positionCount;
			Vector3 sum = Vector3.zero;

			for (int i = 0; i < pointCount; i++)
			{
				sum += lineRenderer.GetPosition(i);
			}

			Vector3 center = sum / pointCount;
			return center;
		}
	}

}
