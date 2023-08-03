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
        
        private int index = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
				ChangeDrawMarkerAddress(blackMarkerAddress);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
				ChangeDrawMarkerAddress(gravityMarkerAddress);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
				ChangeDrawMarkerAddress(rubberMarkerAddress);
            }
        }

        private void ChangeDrawMarkerAddress(string _address)
        {
            drawMarker.LineAddress = _address;
        }
    }   
}
