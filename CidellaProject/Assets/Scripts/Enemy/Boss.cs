using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class Boss : MonoBehaviour
{
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public CallSomethingEvent callSomethingEvent;
    [HideInInspector] public RangeAttackEvent rangeAttackEvent;
    [HideInInspector] public MeleeAttackEvent meleeAttackEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        callSomethingEvent = GetComponent<CallSomethingEvent>();
        rangeAttackEvent = GetComponent<RangeAttackEvent>();
        meleeAttackEvent = GetComponent<MeleeAttackEvent>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        anim = GetComponent<Animator>();
    }
}
