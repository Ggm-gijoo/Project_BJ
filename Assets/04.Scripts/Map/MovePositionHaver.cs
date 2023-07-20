using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public class MovePositionHaver : MonoBehaviour
	{
		[SerializedDictionary("MoveType", "MoveTrm")]
		public SerializedDictionary<MapMoveManager.MoveType, Transform> movePoint = new SerializedDictionary<MapMoveManager.MoveType, Transform> ();
	}
}