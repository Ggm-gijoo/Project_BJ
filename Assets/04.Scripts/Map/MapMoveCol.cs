using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public class MapMoveCol : MonoBehaviour
	{
		[SerializeField]
		private MapMoveManager.MoveType moveType;

		public void OnTriggerEnter(Collider other)
		{
			MapMoveManager.Instance.MoveScene(moveType);
		}
	}
}
