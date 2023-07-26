using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Marker"))
        {
            collision.GetComponent<Marker.BaseMarker>().OnDamaged(10);
        }
    }
}
