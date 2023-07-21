using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public class MapMoveCol : MonoBehaviour
	{
		[SerializeField]
		private MapMoveManager.MoveType moveType;

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.CompareTag("Player"))
			{
				Debug.Log("Move Scene");
				MapMoveManager.Instance.MoveScene(moveType);
			}
		}
	}
}
