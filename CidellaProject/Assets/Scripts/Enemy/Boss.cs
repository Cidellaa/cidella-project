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
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class Boss : MonoBehaviour
{
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public CallSomethingEvent callSomethingEvent;
    [HideInInspector] public RangeAttackEvent rangeAttackEvent;
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        callSomethingEvent = GetComponent<CallSomethingEvent>();
        rangeAttackEvent = GetComponent<RangeAttackEvent>();
        anim = GetComponent<Animator>();
    }
}
