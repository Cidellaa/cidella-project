using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Idle : MonoBehaviour
{
    private IdleEvent idleEvent;
    private Rigidbody2D rb;

    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        idleEvent.OnIdle += IdleEvent_OnIdle;
    }

    private void OnDisable()
    {
        idleEvent.OnIdle -= IdleEvent_OnIdle;
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        MoveRigidbody();
    }

    private void MoveRigidbody()
    {
        rb.velocity = new(0f, rb.velocity.y);
    }
}
