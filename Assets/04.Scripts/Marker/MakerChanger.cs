using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marker
{
    public class MakerChanger : MonoBehaviour
    {
        [SerializeField] private string blackMarkerAddress = "Marker";
        [SerializeField] private string gravityMarkerAddress = "GravityMarker";
        [SerializeField] private string rubberMarkerAddress = "RubberMarker";
        [SerializeField] private DrawMarker drawMarker;

        [SerializeField] private Texture2D blackMarkerCursor;
        [SerializeField] private Texture2D gravityMarkerCursor;
        [SerializeField] private Texture2D rubberMarkerCursor;

        private int index = 0;

        private void Start()
		{
			ChangeDrawMarkerAddress(blackMarkerAddress, blackMarkerCursor);
		}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
				ChangeDrawMarkerAddress(MarkerType.Black);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
				ChangeDrawMarkerAddress(MarkerType.Gravity);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
				ChangeDrawMarkerAddress(MarkerType.Rubber);
            }
        }

        private void ChangeDrawMarkerAddress(MarkerType markerType)
        {
            switch(markerType)
            {
                case MarkerType.Black:
                    ChangeDrawMarkerAddress(blackMarkerAddress, blackMarkerCursor);
					break;
				case MarkerType.Gravity:
					ChangeDrawMarkerAddress(gravityMarkerAddress, gravityMarkerCursor);
					break;
				case MarkerType.Rubber:
					ChangeDrawMarkerAddress(rubberMarkerAddress, rubberMarkerCursor);
					break;
			}
            drawMarker.MarkerType = markerType;
		}
		private void ChangeDrawMarkerAddress(string _address, Texture2D _texture)
		{
			drawMarker.LineAddress = _address;
			Cursor.SetCursor(_texture, new Vector2(0, 0), CursorMode.Auto);
		}
	}
}
