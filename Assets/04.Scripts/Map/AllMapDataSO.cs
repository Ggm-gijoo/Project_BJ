using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	[CreateAssetMenu(fileName = "AllMapDataSO", menuName = "SO/AllMapDataSO")]
	public class AllMapDataSO : ScriptableObject
	{
		public Vector2Int gridSize = new Vector2Int(11, 11); // The number of cells visible in the grid.
		public Dictionary<Vector2, MapDataSO> mapDataDic = new Dictionary<Vector2, MapDataSO>(); // The number of cells visible in the grid.

		[Header("OnlyEditor")]
		public Vector2 editorAddMapDataSO = Vector2.zero;
		public MapDataSO editorMapDataSO = null;

		[ContextMenu("AddMapDataSO")]
		public void AddMapDataSO()
		{
			mapDataDic.Add(editorAddMapDataSO, editorMapDataSO);
		}
	}
}
