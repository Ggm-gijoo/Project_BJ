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

		[SerializeField] private EventSO event_ChangeMarker;

		private int index = 0;

        private void Start()
		{
            //ChangeDrawMarkerAddress(blackMarkerAddress, blackMarkerCursor);
            ChangeDrawMarkerAddress(MarkerType.None);
		}

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && InventoryManager.Instance.inventoryData.isGetBlackMarker)
            {
				ChangeDrawMarkerAddress(MarkerType.Black);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && InventoryManager.Instance.inventoryData.isGetGravityMarker)
            {
				ChangeDrawMarkerAddress(MarkerType.Gravity);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && InventoryManager.Instance.inventoryData.isGetRubberMarker)
            {
				ChangeDrawMarkerAddress(MarkerType.Rubber);
            }
        }

        private void ChangeDrawMarkerAddress(MarkerType markerType)
        {
            switch(markerType)
            {
                case MarkerType.None:
                    break;
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
            event_ChangeMarker.Raise();
		}
		private void ChangeDrawMarkerAddress(string _address, Texture2D _texture)
		{
			drawMarker.LineAddress = _address;
			Cursor.SetCursor(_texture, new Vector2(0, 0), CursorMode.Auto);
		}
	}
}
