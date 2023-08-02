using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEye : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float eyeMultiply = 0.3f;
	private Camera mainCam;

	private void Start()
	{
		mainCam = Camera.main;
	}

	private void Update()
	{
		Vector3 mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCam.transform.position.z));
		
		Vector3 direction = (mousePos - transform.parent.position);
		direction.z = 0f;
		direction = direction.normalized * eyeMultiply;

		// Set the local position of the child object to match the desired distance from the parent
		transform.localPosition = Vector3.Lerp(transform.localPosition, direction, Time.deltaTime * moveSpeed);
	}
}
