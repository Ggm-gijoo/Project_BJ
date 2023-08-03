using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCam : MonoBehaviour
{
	public static InGameCam instance;

	public void Awake()
	{
		instance = this;
	}
}
