using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveFromSwitch : MonoBehaviour
{
	public Vector3 originPos;
	public Vector3 movePos;

	public float duration = 0.5f;

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(movePos, 0.1f);
		if(Application.isPlaying == false)
		{
			originPos = transform.position;
		}
	}

	public void Move()
	{
		transform.DOMove(movePos, duration);
	}

	public void MoveOrigin()
	{
		transform.DOMove(originPos, duration);
	}
}
