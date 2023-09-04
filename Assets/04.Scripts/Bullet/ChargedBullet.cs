using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : MonoBehaviour, IProjectile
{
	public Vector3 Position
	{
		get
		{
			return transform.position;
		}
	}
	public GameObject GameObject
	{
		get
		{
			return gameObject;
		}
	}

	public string Key { get => "ChargeBullet"; }
	private Rigidbody2D rigid;
	[SerializeField]
	private LayerMask colLayerMask;
	private Coroutine poolCoroutine;

	public void PoolThisObject()
	{
		PoolThisObject(gameObject);
	}

	public void PoolThisObject(GameObject gameObject)
	{
		gameObject.SetActive(false);
		ObjectPoolManager.Instance.RegisterObjectAsync(Key, gameObject);
	}

	public void StartMove()
	{
		StartMove(Vector3.zero);
	}

	public void StartMove(Vector3 power)
	{
		rigid ??= GetComponent<Rigidbody2D>();
		rigid.AddForce(power, ForceMode2D.Impulse);
		if (poolCoroutine != null)
		{
			StopCoroutine(poolCoroutine);
		}
		poolCoroutine = StartCoroutine(CountDownPool());
	}

	private IEnumerator CountDownPool()
	{
		yield return new WaitForSeconds(5f);
		if (gameObject.activeSelf)
		{
			PoolThisObject();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		IHitFromBullet hitFromBullet = null;

		if (collision.transform.root.gameObject.TryGetComponent<IHitFromBullet>(out hitFromBullet))
		{
			collision.transform.root.gameObject.GetComponent<IHitFromBullet>().Hit(10, this);
			gameObject.tag = "CanTakeDmg";
		}
		//else if ((colLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
		//{
		//	PoolThisObject();
		//}
	}

	public void CollitionImplement()
	{
		
	}
}
