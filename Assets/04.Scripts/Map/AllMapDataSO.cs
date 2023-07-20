using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using AYellowpaper.SerializedCollections;

namespace Map
{
	[CreateAssetMenu(fileName = "AllMapDataSO", menuName = "SO/AllMapDataSO")]
	public class AllMapDataSO : ScriptableObject
	{
		public Vector2Int gridSize = new Vector2Int(11, 11); // The number of cells visible in the grid.
		[SerializedDictionary("Point", "SO")]
		public AYellowpaper.SerializedCollections.SerializedDictionary<Vector2, MapDataSO> mapDataDic = new AYellowpaper.SerializedCollections.SerializedDictionary<Vector2, MapDataSO>();

		[Header("OnlyEditor")]
		public Vector2 editorAddMapDataSO = Vector2.zero;
		public MapDataSO editorMapDataSO = null;

		[ContextMenu("AddMapDataSO")]
		public void AddMapDataSO()
		{
			mapDataDic.Add(editorAddMapDataSO, editorMapDataSO);

		}

		public void AddMapDataSO(Vector2 pos, MapDataSO mapDataSO)
		{
			mapDataDic.Add(pos, mapDataSO);

		}
	}
}
