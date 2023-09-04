using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float eyeMultiply = 0.3f;
	
	private void Update()
	{
		Vector3 playerPos = PlayerController.instance.transform.position;
		Vector3 direction = (playerPos - transform.parent.position);
		direction.z = 0f;
		direction = direction.normalized * eyeMultiply;

		transform.localPosition = Vector3.Lerp(transform.localPosition, direction, Time.deltaTime * moveSpeed);
	}
}
