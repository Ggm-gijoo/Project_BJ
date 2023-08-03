using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile : IPool
{

	public void StartMove();
	public void StartMove(Vector3 power);
	public virtual void CollitionImplement()
	{
		PoolThisObject();
	}
}

public interface IPool
{
	public string Key { get; }
	public void PoolThisObject();
	public void PoolThisObject(GameObject gameObject);
}