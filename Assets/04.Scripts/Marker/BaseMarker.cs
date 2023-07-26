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

			// LineRenderer로부터 점들을 얻어옵니다.
			Vector3[] linePoints = new Vector3[lineRenderer.positionCount];
			lineRenderer.GetPositions(linePoints);

			// LineRenderer의 선을 감싸는 다각형을 생성합니다.
			Vector2[] polygonPoints = CreatePolygonPoints(linePoints, lineRenderer.startWidth);

			// PolygonCollider2D의 points 배열을 설정합니다.
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
				// 현재 점과 다음 점을 얻습니다.
				Vector3 currentPoint = linePoints[i];
				Vector3 nextPoint = (i < pointCount - 1) ? linePoints[i + 1] : linePoints[i];

				// 현재 점과 다음 점 사이의 방향 벡터를 계산합니다.
				Vector3 direction = (nextPoint - currentPoint).normalized;

				// 선의 방향 벡터의 수직인 벡터를 구합니다.
				Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0f);

				// 선의 양쪽으로 폭을 적용한 점들을 계산합니다.
				Vector3 offset = perpendicular * thickness / 2f;
				Vector3 point1 = currentPoint + offset;
				Vector3 point2 = currentPoint - offset;

				// 다각형의 정점들을 배열에 저장합니다.
				polygonPoints[i] = point1;
				polygonPoints[pointCount * 2 - 1 - i] = point2;
			}

			return polygonPoints;
		}
	}
}
