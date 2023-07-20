using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	public enum IconType
	{
		None,
		Test,
	}

	[CreateAssetMenu(fileName = "MapDataSO", menuName = "SO/MapDataSO")]
	public class MapDataSO : ScriptableObject
	{
		public IconType iconType;
	}
}
