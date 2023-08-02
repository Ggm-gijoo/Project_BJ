using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class MarkerPolyCollider : MonoBehaviour
{
	public LineRenderer lineRenderer;
	public PolygonCollider2D polygonCollider;

}
