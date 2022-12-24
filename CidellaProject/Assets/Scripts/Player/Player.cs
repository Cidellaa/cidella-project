using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(MovementByForce))]
[RequireComponent(typeof(MovementByForceEvent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimatePlayer))]
[DisallowMultipleComponent]
#endregion
public class Player : MonoBehaviour
{
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public MovementByForceEvent movementByForceEvent;
    [HideInInspector] public Animator anim;

    #region Header GROUND CHECK
    [Space(10)]
    [Header("GROUND CHECK")]
    #endregion
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;
    
    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        movementByVelocityEvent = GetComponent<MovementByVelocityEvent>();
        movementByForceEvent = GetComponent<MovementByForceEvent>();
        anim = GetComponent<Animator>();
    }

    public bool OnGround()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsGround);
    }
}
