using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Marker
{
    public class BaseMarker : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private EdgeCollider2D edgeCollider2D;
        private bool isDrawComplete;
        
        public virtual void OnBeginDraw()
        {
            
        }
        
        public virtual void OnDrawing()
        {
        }
        
        public virtual void OnEndDraw()
        {
            isDrawComplete = true;
        }

        public void Update()
        {
            if (!isDrawComplete)
            {
                return;
            }

            //UpdatePositionFromTransform();
        }

        private void UpdatePositionFromTransform()
        {
            int _index = 0;
            Vector3[] _positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(_positions);
            foreach (var pos in _positions)
            {
                Vector3 _newPos = transform.localToWorldMatrix * new Vector4(pos.x, pos.y, pos.z, 1);
                _positions[_index] = _newPos;
                lineRenderer.SetPosition(_index++, _newPos);
            }
            edgeCollider2D.points = toVector2Array(_positions);
        }
        
        public static Vector2[] toVector2Array (Vector3[] v3)
        {
            return System.Array.ConvertAll<Vector3, Vector2> (v3, getV3fromV2);
        }
        
        public static Vector2 getV3fromV2 (Vector3 v3)
        {
            return new Vector2 (v3.x, v3.y);
        }
        
    }
}
