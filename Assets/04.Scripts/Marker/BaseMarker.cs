using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Pool;

namespace Marker
{
	public class BaseMarker : MonoBehaviour, IHitFromBullet, IPool
	{
		public float Gauge
		{
			get
			{
				return gauge;
			}
			set
			{
				gauge = value;
			}
		}

		public LineRenderer LineRenderer => lineRenderer;

		public virtual string Key 
		{ 
			get
			{
				return "Marker";
			}
		}

		[SerializeField] private MarkerType markerType;
		[SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private PolygonCollider2D polygonCollider2D;
		[SerializeField] private MarkerPolyCollider markerPolyCollider;
		[SerializeField] private Material originMaterial;
		[SerializeField] private Material hitMaterial;
		protected bool isDrawComplete;
		protected int hp;
		protected float gauge = 0f;
        
        public virtual void OnBeginDraw()
        {
			hp = 5;
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

		public void Hit(int damage, IProjectile projectile)
		{
			hp -= damage;
			projectile.CollitionImplement();
			if (hp <= 0)
			{
				MarkerManager.Instance.drawMarker.AddGauge(markerType, gauge);
				PoolThisObject();
			}
			else
			{
				StartCoroutine(HitEffect());
			}
		}

		protected IEnumerator HitEffect()
		{
			lineRenderer.transform.DOShakePosition(0.2f, 0.3f, 20).OnComplete(() => 
			{
				lineRenderer.transform.localPosition = Vector3.zero;
			});
			lineRenderer.material = hitMaterial;
			yield return new WaitForSeconds(0.2f);
			lineRenderer.material = originMaterial;
		}

		public void PoolThisObject()
		{
			PoolThisObject(gameObject);
		}

		public void PoolThisObject(GameObject gameObject)
		{
			lineRenderer.material = originMaterial;
			Vector3[] polygonPoints = new Vector3[0];
			Vector2[] polygonPoints2 = new Vector2[0];
			lineRenderer.positionCount = 0;
			lineRenderer.SetPositions(polygonPoints);
			polygonCollider2D.SetPath(0, polygonPoints2);
			gameObject.SetActive(false);
			ObjectPoolManager.Instance.RegisterObjectAsync(Key, gameObject);
		}
	}
}
