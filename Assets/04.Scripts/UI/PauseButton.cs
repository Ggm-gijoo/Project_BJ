using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
	private bool isPause = false;

	public void ContinueBtn()
	{
		GameEventManager.Instance.GetGameEvent("4.Continue").Raise();
	}

	public void OptionBtn()
	{

	}

	public void GotoTitleBtn()
	{
		SceneManager.LoadScene("Title");
	}

	public void PauseProcess()
	{
		Time.timeScale = 0f;
	}

	public void ContinueProcess()
	{
		Time.timeScale = 1f;
	}

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			InputESC();
		}
	}

	public void ChangePause(bool isboolean)
	{
		isPause = isboolean;
	}

	private void InputESC()
	{
		if(isPause)
		{
			ContinueBtn();
		}
		else
		{
			GameEventManager.Instance.GetGameEvent("3.Pause").Raise();
		}
	}

}
