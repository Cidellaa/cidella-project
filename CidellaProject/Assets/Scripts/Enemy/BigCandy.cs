using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BigCandy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 15f;
    private Rigidbody2D rb;
    private Transform boss;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").transform;
        StartCoroutine(StartFalling());
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
    }
}
