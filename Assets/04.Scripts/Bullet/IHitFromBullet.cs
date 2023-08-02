using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitFromBullet
{
	public abstract void Hit(int damage, IProjectile hitObj);
}
