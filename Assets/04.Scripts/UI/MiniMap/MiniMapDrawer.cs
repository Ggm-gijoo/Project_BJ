using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public class MiniMapDrawer : MonoBehaviour
{
	[SerializeField]
	private MiniMapPostIt[] postIts = new MiniMapPostIt[9];
	[SerializeField]
	private AllMapDataSO allMapDataSO;

	public void UpdateUI()
	{
		UpdatePostIt();
	}

	private void UpdatePostIt()
	{
		Vector2 curPos = MapMoveManager.Instance.CurrentPos;
		SetPostIt(curPos + new Vector2(0,0),0);
		SetPostIt(curPos + new Vector2(-1,0),1); //����
		SetPostIt(curPos + new Vector2(1,0),2); //������
		SetPostIt(curPos + new Vector2(-1,1),3); //���� ��
		SetPostIt(curPos + new Vector2(1,1),4); //������ ��
		SetPostIt(curPos + new Vector2(0,1),5); // ��
		SetPostIt(curPos + new Vector2(-1,-1),6); //���� �Ʒ�
		SetPostIt(curPos + new Vector2(1,-1),7); //������ �Ʒ�
		SetPostIt(curPos + new Vector2(0,-1), 8); // �Ʒ�
	}

	private void SetPostIt(Vector2 pos, int number)
	{
		if(allMapDataSO.mapDataDic.ContainsKey(pos))
		{
			postIts[number].gameObject.SetActive(true);
			postIts[number].SetMapDataSO(allMapDataSO.mapDataDic[pos]);
		}
		else
		{
			postIts[number].gameObject.SetActive(false);
		}

	}
}
