using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueItem : MonoBehaviour
{
	public static DialogueSO curDialogueSO;
	[SerializeField]
	private DialogueSO dialogueSO;

	private void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			if(Input.GetKeyDown(KeyCode.F) && !DialoguePanel.isDialoguing)
			{
				curDialogueSO = dialogueSO;
				GameEventManager.Instance.GetGameEvent("5.DialogueStart").Raise();
			}
		}
	}
}
