using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHUD : MonoBehaviour
{
	public void Awake()
	{
		SceneManager.LoadScene("HUDScene", LoadSceneMode.Additive);
	}
}
