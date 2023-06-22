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
        
        public override void OnEndDraw()
        {
            edgeCollider2D.enabled = true;
            rigid.isKinematic = false;
        }
    }

}
