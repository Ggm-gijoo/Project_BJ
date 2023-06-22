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
        private EdgeCollider2D edgeCollider2D;
        [SerializeField]
        private PolygonCollider2D polygonCollider2D;
        
        
        public override void OnEndDraw()
        {
            base.OnEndDraw();
            edgeCollider2D.enabled = true;

            //polygonCollider2D.points = edgeCollider2D.points;
            rigid.isKinematic = false;
        }
        
        //public void Update()
        //{
        //    
        //}
    }

}
