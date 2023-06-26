using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
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
            GameObject bulletClone = GameObject.Instantiate(bullet, transform.position + dir * Vector3.right * 2f, Quaternion.identity);
            bulletClone.GetComponent<Rigidbody2D>().AddForce(dir * Vector2.right * 5, ForceMode2D.Impulse);
            yield return new WaitForSeconds(2f);
        }
    }
}
