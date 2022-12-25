using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(BossAI))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(CallSomething))]
[RequireComponent(typeof(CallSomethingEvent))]
[RequireComponent(typeof(RangeAttack))]
[RequireComponent(typeof(RangeAttackEvent))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(MeleeAttackEvent))]
[RequireComponent(typeof(MovementToPosition))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(DestroyedEvent))]
[RequireComponent(typeof(AnimateBoss))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
#endregion
public class Boss : MonoBehaviour
{
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public CallSomethingEvent callSomethingEvent;
    [HideInInspector] public RangeAttackEvent rangeAttackEvent;
    [HideInInspector] public MeleeAttackEvent meleeAttackEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public BossAI bossAI;
    [HideInInspector] public int currentHealth;

    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        callSomethingEvent = GetComponent<CallSomethingEvent>();
        rangeAttackEvent = GetComponent<RangeAttackEvent>();
        meleeAttackEvent = GetComponent<MeleeAttackEvent>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        healthEvent = GetComponent<HealthEvent>();
        anim = GetComponent<Animator>();
        bossAI = GetComponent<BossAI>();
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
        currentHealth = healthEventArgs.healthAmount;
        if (healthEventArgs.healthAmount <= 0)
        {
            destroyedEvent.CallDestroyedEvent(true);
            bossAI.DisableBoss();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}