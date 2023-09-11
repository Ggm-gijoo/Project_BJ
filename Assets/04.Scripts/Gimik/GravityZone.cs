using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
	public Vector2 gravityDir;
	private Dictionary<IGravity, Vector3> originGravity;

	private void Start()
	{
		originGravity = new Dictionary<IGravity, Vector3>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<IGravity>(out var gravity))
		{
			if(!originGravity.ContainsKey(gravity))
			{
				originGravity.Add(gravity, gravity.GravityDir);
			}
			gravity.GravityDir = gravityDir;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent<IGravity>(out var gravity))
		{
			if (originGravity.ContainsKey(gravity))
			{
				gravity.GravityDir = originGravity[gravity];
				originGravity.Remove(gravity);
			}
		}
	}
}
