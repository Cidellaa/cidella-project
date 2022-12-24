using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    #region Header MOVEMENT
    [Space(5)]
    [Header("MOVEMENT")]
    #endregion
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedInAir;
    [SerializeField] private float jumpForce;

    #region Header ATTACK
    [Space(10)]
    [Header("ATTACK")]
    #endregion
    [SerializeField] private GameObject rangeWeaponPrefab;
    [SerializeField] private Transform rangeWeaponPosition;
    [SerializeField] private float rangeWeaponSpeed = 12f;

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
        AttackInput();
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
            if (player.OnGround())
                player.movementByVelocityEvent.CallMovementByVelocityEvent(moveSpeed, horizontalMovement);
            else
                player.movementByVelocityEvent.CallMovementByVelocityEvent(moveSpeedInAir, horizontalMovement);
        }
        else
        {   
            player.idleEvent.CallIdleEvent();
        }
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump") && player.OnGround())
        {
            player.movementByForceEvent.CallMovementByForceEvent(Vector2.up, jumpForce);
        }
    }

    private void AttackInput()
    {
        bool leftMouseButtonDown = Input.GetMouseButtonDown(0);
        bool rightMouseButtonDown = Input.GetMouseButtonDown(1);
        
        if (player.OnGround())
        {
            if (leftMouseButtonDown)
            {

            }
            else if (rightMouseButtonDown)
            {
                player.rangeAttackEvent.CallRangeAttackEvent(rangeWeaponPrefab, rangeWeaponPosition, transform.localScale, rangeWeaponSpeed);
            }
        }
    }
}
