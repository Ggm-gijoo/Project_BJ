using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

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
        mapObj.transform.DOKill();
		mapObj.transform.DOLocalMoveX(3.4f, 1f);
		isOpen = true;
	}

    private void CloseMap()
	{
		mapObj.transform.DOKill();
		mapObj.transform.DOLocalMoveX(12f, 1f).OnComplete(() => mapObj.SetActive(false));
		isOpen = false;
	}
}
