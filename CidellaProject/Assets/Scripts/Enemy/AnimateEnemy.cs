using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateEnemy : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        enemy.idleEvent.OnIdle += IdleEvent_OnIdle;
        enemy.rangeAttackEvent.OnRangeAttack += RangeAttackEvent_OnRangeAttack;
        enemy.meleeAttackEvent.OnMeleeAttack += MeleeAttackEvent_OnMeleeAttack;
        enemy.movementToPositionEvent.OnMovementToPosition += MovementToPositionEvent_OnMovementToPosition;
        enemy.destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
    }

    private void OnDisable()
    {
        enemy.idleEvent.OnIdle -= IdleEvent_OnIdle;
        enemy.rangeAttackEvent.OnRangeAttack -= RangeAttackEvent_OnRangeAttack;
        enemy.meleeAttackEvent.OnMeleeAttack -= MeleeAttackEvent_OnMeleeAttack;
        enemy.movementToPositionEvent.OnMovementToPosition -= MovementToPositionEvent_OnMovementToPosition;
        enemy.destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyed;
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        SetIdleAnimationParameters();
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
        enemy.anim.SetBool(Settings.isMoving, false);
        enemy.anim.SetBool(Settings.isIdle, true);
    }

    private void SetRangeAttackAnimationParameters()
    {
        enemy.anim.SetBool(Settings.isMoving, false);
        enemy.anim.SetBool(Settings.isIdle, false);
        enemy.anim.SetTrigger(Settings.isRangeAttacking);
    }

    private void SetMeleeAttackAnimationParameters()
    {
        enemy.anim.SetBool(Settings.isMoving, false);
        enemy.anim.SetBool(Settings.isIdle, false);
        enemy.anim.SetTrigger(Settings.isMeleeAttacking);
    }

    private void SetMovementToPositionAnimationParameters()
    {
        enemy.anim.SetBool(Settings.isIdle, false);
        enemy.anim.SetBool(Settings.isMoving, true);
    }

    private void SetDeathAnimationParameters()
    {
        enemy.anim.SetBool(Settings.isIdle, false);
        enemy.anim.SetBool(Settings.isMoving, false);
        enemy.anim.SetBool(Settings.isDead, true);
    }
}
