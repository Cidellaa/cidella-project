using UnityEngine;

[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class MovementByVelocity : MonoBehaviour
{
    private MovementByVelocityEvent movementByVelocityEvent;
    private Rigidbody2D rb;

    private void Awake()
    {
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void OnDisable()
    {
        movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityEventArgs movementByVelocityEventArgs)
    {
        MoveRigidbody(movementByVelocityEventArgs.speed, movementByVelocityEventArgs.direction);
        Flip(movementByVelocityEventArgs.direction);
    }

    private void MoveRigidbody(float speed, float direction)
    {
        rb.velocity = new(speed * direction, rb.velocity.y);
    }

    private void Flip(float direction)
    {
        if (direction >= 0.1f)
            transform.localScale = Vector3.one;
        else if (direction <= -0.1f)
            transform.localScale = new(-1, 1, 1);
    }
}
