using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;
using DG.Tweening;

public class HPObject : MonoBehaviour, IHitFromBullet
{
	[SerializeField] protected int hp = 5;
	[SerializeField] protected Renderer model;
	[SerializeField] protected Material originMaterial;
	[SerializeField] protected Material hitMaterial;

	public void Hit(int damage, IProjectile projectile)
	{
		hp -= damage;
		projectile.CollitionImplement();
		if (hp <= 0)
		{
			EffectManager.Instance.SetEffectDefault("DestroyMarkerEffect", projectile.Position, Quaternion.identity);
			Destroy();
		}
		else
		{
			StartCoroutine(HitEffect());
		}
	}

	protected IEnumerator HitEffect()
	{
		Sound.SoundManager.Instance.PlayEFF("GroundHit");
		model.transform.DOShakePosition(0.2f, 0.3f, 20).OnComplete(() =>
		{
			model.transform.localPosition = Vector3.zero;
		});
		model.material = hitMaterial;
		yield return new WaitForSeconds(0.2f);
		model.material = originMaterial;
	}

	protected void Destroy()
	{
		gameObject.SetActive(false);
	}
}
