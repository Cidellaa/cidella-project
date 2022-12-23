using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        MovementInput();
        JumpInput();
    }

    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (horizontalMovement != 0)
        {
            player.movementByVelocityEvent.CallMovementByVelocityEvent(moveSpeed, horizontalMovement);
        }
        else
            player.idleEvent.CallIdleEvent();
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump") && player.OnGround())
        {
            player.movementByForceEvent.CallMovementByForceEvent(Vector2.up, jumpForce);
        }
    }
}
