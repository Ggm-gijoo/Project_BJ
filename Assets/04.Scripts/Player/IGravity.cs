using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGravity
{
	public Vector3 Gravity
	{
		get
		{
			return GravityDir;
		}
	}

	public Vector3 GravityDir
	{ get; set; }
}
