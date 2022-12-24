using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigCandy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;

    private Transform boss;
    private bool wasCollided;

    private Rigidbody2D rb;
    private Collider2D col;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        StartCoroutine(StartFalling());
        Destroy(gameObject, 7f);
    }

    private IEnumerator StartFalling()
    {
        transform.DOMoveY(transform.position.y - 1.5f, 2f);
        yield return new WaitForSeconds(1f);

        while (transform.position.y >= boss.position.y)
        {
            rb.velocity = Vector2.down * moveSpeed;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!wasCollided && collision.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(2);
            wasCollided = true;
        }
    }
}