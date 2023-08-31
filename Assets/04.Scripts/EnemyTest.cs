using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionType
{
    TwoSided,
    Targetted
}

public class EnemyTest : MonoBehaviour
{
    [SerializeField] private string bullet;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletDelay = 2f;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] DirectionType directionType;

    private Transform playerTransform;
    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(Fire());
    }
    public IEnumerator Fire()
    {
        while(true)
        {
            try
			{
                Vector2 dir = Vector2.zero;
				switch (directionType)
                {
                    case DirectionType.TwoSided:
						float angle = Mathf.Sign(playerTransform.position.x - transform.position.x);
                        dir = angle * Vector2.right;
						break;
                    case DirectionType.Targetted:
						dir = (playerTransform.position - transform.position).normalized;
						break;
                }
				GameObject bulletClone = ObjectPoolManager.Instance.GetObject(bullet, transform.position, Quaternion.identity);
                bulletClone.tag = "EnemyWeapon";
				bulletClone.GetComponent<IProjectile>().StartMove(dir * bulletSpeed);
			}
            catch
            {

            }
            yield return new WaitForSeconds(bulletDelay);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CanDestroy") || collision.gameObject.CompareTag("CanTakeDmg") || collision.transform.CompareTag("PlayerWeapon"))
        {
            deathParticle.SetActive(true);
            deathParticle.transform.SetParent(null);
            Destroy(gameObject);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("CanDestroy") || collision.gameObject.CompareTag("CanTakeDmg") || collision.transform.CompareTag("PlayerWeapon"))
		{
			deathParticle.SetActive(true);
			deathParticle.transform.SetParent(null);
			Destroy(gameObject);
		}
	}
}
