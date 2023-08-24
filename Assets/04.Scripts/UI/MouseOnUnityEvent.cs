using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseOnUnityEvent : MonoBehaviour, IPointerEnterHandler
{
    public UnityEvent mouseEvent;

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		mouseEvent?.Invoke();
	}
}
