using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
