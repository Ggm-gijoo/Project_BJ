using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialoguePanel : MonoBehaviour
{
    public static bool isDialoguing;
    [SerializeField]
    private GameObject panelObj;
	[SerializeField]
    private TextMeshProUGUI dialogueText;


    public void ShowText()
    {
		dialogueText.text = "";
        isDialoguing = true;
		panelObj.SetActive(true);
		StartCoroutine(Typing());
	}

    private IEnumerator Typing()
    {
        foreach(string str in DialogueItem.curDialogueSO.talkList)
		{
			dialogueText.text = "";
            for(int i = 0; i < str.Length; ++i)
            {
                dialogueText.text += str[i];
                yield return new WaitForSeconds(0.05f);
            }
            while(true)
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    break;
				}
				yield return null;
			}
		}
		GameEventManager.Instance.GetGameEvent("6.DialogueEnd").Raise();
		isDialoguing = false;
		panelObj.SetActive(false);
	}
}
