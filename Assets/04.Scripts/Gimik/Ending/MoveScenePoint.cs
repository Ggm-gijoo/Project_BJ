using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScenePoint : MonoBehaviour
{
	[SerializeField]
	private string sceneName;

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			MoveEndScene();
		}
	}
	public void MoveEndScene()
	{
		SceneManager.LoadScene(sceneName);
	}
}
