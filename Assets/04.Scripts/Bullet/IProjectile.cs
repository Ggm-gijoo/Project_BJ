using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
	public string Key { get; }

	public void StartMove();
	public void StartMove(Vector3 power);
	public void PoolThisObject();
	public void PoolThisObject(GameObject gameObject);
	public virtual void CollitionImplement()
	{
		PoolThisObject();
	}
}
