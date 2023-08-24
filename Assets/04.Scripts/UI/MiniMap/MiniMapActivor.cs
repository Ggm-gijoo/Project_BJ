using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniMapActivor : MonoBehaviour
{
    public UnityEvent openEvent;
    [SerializeField]
    private GameObject mapObj;
    private bool isOpen;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(isOpen)
            {
                CloseMap();
            }
            else
            {
                OpenMap();
            }
		}
    }

    //
    private void OpenMap()
    {
        openEvent?.Invoke();
		mapObj.SetActive(true);
		isOpen = true;
	}

    private void CloseMap()
	{
		mapObj.SetActive(false);
		isOpen = false;
	}
}
