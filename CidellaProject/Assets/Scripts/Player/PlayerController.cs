using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedInAir;
    [SerializeField] private float jumpForce;

    private bool isPlayerMovementDisabled;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (isPlayerMovementDisabled) return;

        MovementInput();
        JumpInput();
    }

    public void EnablePlayer()
    {
        isPlayerMovementDisabled = true;
    }

    public void DisablePlayer()
    {
        isPlayerMovementDisabled = true;
    }

    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (horizontalMovement != 0)
        {
            if(player.OnGround())
                player.movementByVelocityEvent.CallMovementByVelocityEvent(moveSpeed, horizontalMovement);
            else
                player.movementByVelocityEvent.CallMovementByVelocityEvent(moveSpeedInAir, horizontalMovement);
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
