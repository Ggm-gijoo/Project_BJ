using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Marker"))
        {
            collision.gameObject.GetComponent<Marker.BaseMarker>().OnDamaged(1);

            if (collision.gameObject.GetComponent<Marker.RubberMarker>() == null)
                Destroy(gameObject);
        }
    }
}
