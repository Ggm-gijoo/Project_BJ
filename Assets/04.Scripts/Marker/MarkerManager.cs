using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Pattern;

namespace Marker
{
	public class MarkerManager : MonoSingleton<MarkerManager>
	{
		public DrawMarker drawMarker;
		public MakerChanger makerChanger;
	}
}
