using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Map.MapMoveManager;

namespace Map
{
	public class MovePositionHaver : MonoBehaviour
	{
		[SerializedDictionary("MoveType", "MoveTrm")]
		public SerializedDictionary<MapMoveManager.MoveType, Transform> movePoint = new SerializedDictionary<MapMoveManager.MoveType, Transform> ();

		private void Start()
		{
			//플레이어 위치 변경

			Vector2 movePoint = Vector2.zero;
			MovePositionHaver movePositionHaver = GameObject.FindObjectOfType<MovePositionHaver>();
			switch (MapMoveManager.Instance.CurrentMoveType)
			{
				case MoveType.Left:
					movePoint = movePositionHaver.movePoint[MoveType.Right].position;
					break;
				case MoveType.Right:
					movePoint = movePositionHaver.movePoint[MoveType.Left].position;
					break;
				case MoveType.Up:
					movePoint = movePositionHaver.movePoint[MoveType.Down].position;
					break;
				case MoveType.Down:
					movePoint = movePositionHaver.movePoint[MoveType.Up].position;
					break;
			}
			GameObject.FindGameObjectWithTag("Player").transform.position = movePoint;
		}
	}
}