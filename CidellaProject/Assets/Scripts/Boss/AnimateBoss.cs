using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Boss))]
[DisallowMultipleComponent]
public class AnimateBoss : MonoBehaviour
{
    private Boss boss;

    private void Awake()
    {
        boss = GetComponent<Boss>();
    }

    private void OnEnable()
    {
        boss.idleEvent.OnIdle += IdleEvent_OnIdle;
        boss.callSomethingEvent.OnCallSomething += CallSomethingEvent_OnCallSomething;
        boss.rangeAttackEvent.OnRangeAttack += RangeAttackEvent_OnRangeAttack;
        boss.meleeAttackEvent.OnMeleeAttack += MeleeAttackEvent_OnMeleeAttack;
        boss.movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;
        boss.destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
    }

    private void OnDisable()
    {
        boss.idleEvent.OnIdle -= IdleEvent_OnIdle;
        boss.callSomethingEvent.OnCallSomething -= CallSomethingEvent_OnCallSomething;
        boss.rangeAttackEvent.OnRangeAttack -= RangeAttackEvent_OnRangeAttack;
        boss.meleeAttackEvent.OnMeleeAttack -= MeleeAttackEvent_OnMeleeAttack;
        boss.movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        SetIdleAnimationParameters();
    }

    private void CallSomethingEvent_OnCallSomething(CallSomethingEvent callSomethingEvent, CallSomethingEventArgs callSomethingEventArgs)
    {
        SetCallSomethingAnimationParameters();
    }

    private void RangeAttackEvent_OnRangeAttack(RangeAttackEvent rangeAttackEvent, RangeAttackEventArgs rangeAttackEventArgs)
    {
        SetRangeAttackAnimationParameters();
    }

    private void MeleeAttackEvent_OnMeleeAttack(MeleeAttackEvent meleeAttackEvent, MeleeAttackEventArgs meleeAttackEventArgs)
    {
        SetMeleeAttackAnimationParameters();
    }

    private void MovementToPositionEvent_OnMovementToPosition(MovementToPositionEvent movementToPositionEvent, MovementToPositionEventArgs movementToPositionEventArgs)
    {
        SetMovementToPositionAnimationParameters();
    }

    private void DestroyedEvent_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        SetDeathAnimationParameters();
    }

    private void SetIdleAnimationParameters()
    {
        boss.anim.SetBool(Settings.isMoving, false);
        boss.anim.SetBool(Settings.isIdle, true);
    }
    
    private void SetCallSomethingAnimationParameters()
    {
        boss.anim.SetBool(Settings.isMoving, false);
        boss.anim.SetBool(Settings.isIdle, false);
        boss.anim.SetTrigger(Settings.isCallingSomething);
    }

    private void SetRangeAttackAnimationParameters()
    {
        boss.anim.SetBool(Settings.isMoving, false);
        boss.anim.SetBool(Settings.isIdle, false);
        boss.anim.SetTrigger(Settings.isRangeAttacking);
    }

    private void SetMeleeAttackAnimationParameters()
    {
        boss.anim.SetBool(Settings.isMoving, false);
        boss.anim.SetBool(Settings.isIdle, false);
        boss.anim.SetTrigger(Settings.isMeleeAttacking);
    }

    private void SetMovementToPositionAnimationParameters()
    {
        boss.anim.SetBool(Settings.isIdle, false);
        boss.anim.SetBool(Settings.isMoving, true);
    }

    private void SetDeathAnimationParameters()
    {
        boss.anim.SetBool(Settings.isIdle, false);
        boss.anim.SetBool(Settings.isMoving, false);
        boss.anim.SetBool(Settings.isDead, true);
    }
}
