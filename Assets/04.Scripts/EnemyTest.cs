using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] private string bullet;
    [SerializeField] private GameObject deathParticle;

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
            float dir = Mathf.Sign(playerTransform.position.x - transform.position.x);
            GameObject bulletClone = ObjectPoolManager.Instance.GetObject(bullet, transform.position + dir * Vector3.right * 2f, Quaternion.identity);
            bulletClone.GetComponent<IProjectile>().StartMove(dir * Vector2.right * 5);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CanDestroy") || collision.gameObject.CompareTag("CanTakeDmg"))
        {
            deathParticle.SetActive(true);
            deathParticle.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
