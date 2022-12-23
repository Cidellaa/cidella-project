using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementByForceEvent))]
[DisallowMultipleComponent]
public class MovementByForce : MonoBehaviour
{
    private Rigidbody2D rb;
    private MovementByForceEvent movementByForceEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementByForceEvent = GetComponent<MovementByForceEvent>();
    }

    private void OnEnable()
    {
        movementByForceEvent.OnMovementByForce += MovementByForceEvent_OnMovementByForce;
    }

    private void OnDisable()
    {
        movementByForceEvent.OnMovementByForce -= MovementByForceEvent_OnMovementByForce;
    }

    private void MovementByForceEvent_OnMovementByForce(MovementByForceEvent movementByForceEvent, MovementByForceEventArgs movementByForceEventArgs)
    {
        MoveRigidbody(movementByForceEventArgs.direction, movementByForceEventArgs.force);
    }

    private void MoveRigidbody(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
