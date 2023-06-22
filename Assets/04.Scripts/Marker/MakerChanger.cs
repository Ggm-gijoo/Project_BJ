using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Marker
{
    public class MakerChanger : MonoBehaviour
    {
        [SerializeField] private GameObject blackMarkerPrefab;
        [SerializeField] private GameObject gravityMarkerPrefab;
        [SerializeField] private GameObject rubberMarkerPrefab;
        [SerializeField] private DrawMarker drawMarker;
        
        private int index = 0;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeDrawMarkerPrefab(blackMarkerPrefab);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeDrawMarkerPrefab(gravityMarkerPrefab);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeDrawMarkerPrefab(rubberMarkerPrefab);
            }
        }

        private void ChangeDrawMarkerPrefab(GameObject _prefab)
        {
            drawMarker.LinePrefab = _prefab;
        }
    }   
}
