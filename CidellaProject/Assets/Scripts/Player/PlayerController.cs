using System.Collections;
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

    #region Header RANGE ATTACK
    [Space(10)]
    [Header("RANGE ATTACK")]
    #endregion
    [SerializeField] private GameObject rangeWeaponPrefab;
    [SerializeField] private Transform rangeWeaponPosition;
    [SerializeField] private float rangeWeaponSpeed = 12f;

    #region Header ATTACK
    [Space(10)]
    [Header("ATTACK")]
    #endregion
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private LayerMask whatIsEnemy;

    private readonly float attackWaitTimer = .4f;

    private bool isPlayerMovementDisabled;

    private Player player;
    private Rigidbody2D rb;

    private void Awake()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
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
        isPlayerMovementDisabled = false;
    }

    public void DisablePlayer()
    {
        rb.velocity = Vector2.zero;
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
                StartCoroutine(MeleeAttackRoutine());
            }
            else if (rightMouseButtonDown)
            {
                StartCoroutine(RangeAttackRoutine());
            }
        }
    }

    private IEnumerator MeleeAttackRoutine()
    {
        DisablePlayer();
        player.meleeAttackEvent.CallMeleeAttack(meleeAttackPosition, meleeAttackRadius, whatIsEnemy);
        yield return new WaitForSeconds(attackWaitTimer);

        EnablePlayer();
    }

    private IEnumerator RangeAttackRoutine()
    {
        DisablePlayer();
        player.rangeAttackEvent.CallRangeAttackEvent(rangeWeaponPrefab, rangeWeaponPosition, transform.localScale, rangeWeaponSpeed);
        yield return new WaitForSeconds(attackWaitTimer);

        EnablePlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackRadius);
    }
}
