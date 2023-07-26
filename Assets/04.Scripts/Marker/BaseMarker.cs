using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marker
{
    public class BaseMarker : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private PolygonCollider2D polygonCollider2D;
		[SerializeField] private MarkerPolyCollider markerPolyCollider;
		private bool isDrawComplete;
		private int hp;
        
        public virtual void OnBeginDraw()
        {
			hp = 7;
        }
        
        public virtual void OnDrawing()
        {
        }
        
        public virtual void OnEndDraw()
        {
            isDrawComplete = true;
			markerPolyCollider.enabled = true;
            polygonCollider2D.enabled = true;

			// LineRenderer�κ��� ������ ���ɴϴ�.
			Vector3[] linePoints = new Vector3[lineRenderer.positionCount];
			lineRenderer.GetPositions(linePoints);

			// LineRenderer�� ���� ���δ� �ٰ����� �����մϴ�.
			Vector2[] polygonPoints = CreatePolygonPoints(linePoints, lineRenderer.startWidth);

			// PolygonCollider2D�� points �迭�� �����մϴ�.
			polygonCollider2D.SetPath(0, polygonPoints);
		}

		public virtual void OnDamaged(int dmg)
        {
			hp -= dmg;
			if (hp <= 0)
				Destroy(gameObject);
        }

        public void Update()
        {
            if (!isDrawComplete)
            {
                return;
            }

            //UpdatePositionFromTransform();
        }
		private Vector2[] CreatePolygonPoints(Vector3[] linePoints, float thickness)
		{
			int pointCount = linePoints.Length;
			Vector2[] polygonPoints = new Vector2[pointCount * 2];

			for (int i = 0; i < pointCount; i++)
			{
				// ���� ���� ���� ���� ����ϴ�.
				Vector3 currentPoint = linePoints[i];
				Vector3 nextPoint = (i < pointCount - 1) ? linePoints[i + 1] : linePoints[i];

				// ���� ���� ���� �� ������ ���� ���͸� ����մϴ�.
				Vector3 direction = (nextPoint - currentPoint).normalized;

				// ���� ���� ������ ������ ���͸� ���մϴ�.
				Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0f);

				// ���� �������� ���� ������ ������ ����մϴ�.
				Vector3 offset = perpendicular * thickness / 2f;
				Vector3 point1 = currentPoint + offset;
				Vector3 point2 = currentPoint - offset;

				// �ٰ����� �������� �迭�� �����մϴ�.
				polygonPoints[i] = point1;
				polygonPoints[pointCount * 2 - 1 - i] = point2;
			}

			return polygonPoints;
		}
	}
}
