using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(MovementByForce))]
[RequireComponent(typeof(MovementByForceEvent))]
[RequireComponent(typeof(RangeAttack))]
[RequireComponent(typeof(RangeAttackEvent))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(DestroyedEvent))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(MeleeAttackEvent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimatePlayer))]
[DisallowMultipleComponent]
#endregion
public class Player : MonoBehaviour
{
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public MovementByForceEvent movementByForceEvent;
    [HideInInspector] public RangeAttackEvent rangeAttackEvent;
    [HideInInspector] public Health health;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public MeleeAttackEvent meleeAttackEvent;
    [HideInInspector] public PlayerController playerController;
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
        rangeAttackEvent = GetComponent<RangeAttackEvent>();
        health = GetComponent<Health>();
        healthEvent = GetComponent<HealthEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        meleeAttackEvent = GetComponent<MeleeAttackEvent>();
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
    }

    private void OnDisable()
    {
        healthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
    }

    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        if (healthEventArgs.healthAmount <= 0)
            destroyedEvent.CallDestroyedEvent(true);
    }

    public bool OnGround()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsGround);
    }
}
