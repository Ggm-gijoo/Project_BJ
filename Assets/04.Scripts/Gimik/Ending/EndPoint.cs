using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			MoveEndScene();
		}
	}
	public void MoveEndScene()
	{
		SceneManager.LoadScene("EndScene");
	}
}
