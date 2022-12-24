using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class MovementToPosition : MonoBehaviour
{
    private MovementToPositionEvent movementToPositionEvent;
    private Rigidbody2D rb;

    private void Awake()
    {
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;
    }

    private void OnDisable()
    {
        movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;
    }

    private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent movementToPositionEvent, MovementToPositionEventArgs movementToPositionEventArgs)
    {
        MoveRigidbody(movementToPositionEventArgs.moveSpeed,movementToPositionEventArgs.currentPosition, movementToPositionEventArgs.targetPosition);
    }

    private void MoveRigidbody(float moveSpeed, Vector3 currentPosition, Vector3 targetPosition)
    {
        Vector2 unitVector = Vector3.Normalize(targetPosition - currentPosition);
        rb.MovePosition(rb.position + (Time.fixedDeltaTime * moveSpeed * unitVector));
    }
}
