using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region REQUIRE COMPONENTS
[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(HealthEvent))]
[RequireComponent(typeof(MovementToPosition))]
[RequireComponent(typeof(MovementToPositionEvent))]
[RequireComponent(typeof(RangeAttack))]
[RequireComponent(typeof(RangeAttackEvent))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(MeleeAttackEvent))]
[RequireComponent(typeof(DestroyedEvent))]
[RequireComponent(typeof(AnimateEnemy))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
#endregion
public class Enemy : MonoBehaviour
{
    [HideInInspector] public IdleEvent idleEvent;
    [HideInInspector] public HealthEvent healthEvent;
    [HideInInspector] public MovementToPositionEvent movementToPositionEvent;
    [HideInInspector] public RangeAttackEvent rangeAttackEvent;
    [HideInInspector] public MeleeAttackEvent meleeAttackEvent;
    [HideInInspector] public DestroyedEvent destroyedEvent;
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        idleEvent = GetComponent<IdleEvent>();
        healthEvent = GetComponent<HealthEvent>();
        movementToPositionEvent = GetComponent<MovementToPositionEvent>();
        rangeAttackEvent = GetComponent<RangeAttackEvent>();
        meleeAttackEvent = GetComponent<MeleeAttackEvent>();
        destroyedEvent = GetComponent<DestroyedEvent>();
        anim = GetComponent<Animator>();
    }

}