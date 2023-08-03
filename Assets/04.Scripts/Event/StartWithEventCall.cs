using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWithEventCall : MonoBehaviour
{
    [SerializeField] private EventSO eventSO;

    // Start is called before the first frame update
    void Start()
    {
        eventSO.Raise();
	}
}
