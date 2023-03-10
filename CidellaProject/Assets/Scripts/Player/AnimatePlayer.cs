using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class AnimatePlayer : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.idleEvent.OnIdle += IdleEvent_OnIdle;
        player.movementByVelocityEvent.OnMovementByVelocity += MovementByVelocityEvent_OnMovementByVelocity;
        player.movementByForceEvent.OnMovementByForce += MovementByForceEvent_OnMovementByForce;
        player.meleeAttackEvent.OnMeleeAttack += MeleeAttackEvent_OnMeleeAttack;
        player.rangeAttackEvent.OnRangeAttack += RangeAttackEvent_OnRangeAttack;
        player.destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
    }


    private void OnDisable()
    {
        player.idleEvent.OnIdle -= IdleEvent_OnIdle;
        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        player.movementByForceEvent.OnMovementByForce -= MovementByForceEvent_OnMovementByForce;
        player.meleeAttackEvent.OnMeleeAttack -= MeleeAttackEvent_OnMeleeAttack;
        player.rangeAttackEvent.OnRangeAttack -= RangeAttackEvent_OnRangeAttack;
        player.destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyed;
    }

    private void IdleEvent_OnIdle(IdleEvent idleEvent)
    {
        SetIdleAnimationParameters();
    }

    private void MovementByVelocityEvent_OnMovementByVelocity(MovementByVelocityEvent movementByVelocityEvent, MovementByVelocityEventArgs movementByVelocityEventArgs)
    {
        SetMovementAnimationParameters();
    }

    private void MovementByForceEvent_OnMovementByForce(MovementByForceEvent movementByForceEvent, MovementByForceEventArgs movementByForceEventArgs)
    {
        SetMovementByForceAnimationParameters();
    }

    private void MeleeAttackEvent_OnMeleeAttack(MeleeAttackEvent meleeAttackEvent, MeleeAttackEventArgs meleeAttackEventArgs)
    {
        SetMeleeAttackAnimationParameters();
    }
   
    private void RangeAttackEvent_OnRangeAttack(RangeAttackEvent rangeAttackEvent, RangeAttackEventArgs rangeAttackEventArgs)
    {
        SetRangeAttackAnimationParameters();
    }
    
    private void DestroyedEvent_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        SetDeathAnimationParameters();
    }

    private void SetIdleAnimationParameters()
    {
        if (!player.OnGround())
        {
            Debug.Log("Jump");
            SetMovementByForceAnimationParameters();
            return;
        }
        player.anim.SetBool(Settings.isMoving, false);
        player.anim.SetBool(Settings.isJumping, false);
        player.anim.SetBool(Settings.isIdle, true);
    }

    private void SetMovementAnimationParameters()
    {
        if (!player.OnGround())
        {
            SetMovementByForceAnimationParameters();
            return;
        }
        player.anim.SetBool(Settings.isIdle, false);
        player.anim.SetBool(Settings.isJumping, false);
        player.anim.SetBool(Settings.isMoving, true);
    }

    private void SetMovementByForceAnimationParameters()
    {
        player.anim.SetBool(Settings.isIdle, false);
        player.anim.SetBool(Settings.isMoving, false);
        player.anim.SetBool(Settings.isJumping, true);
    }

    private void SetMeleeAttackAnimationParameters()
    {
        player.anim.SetBool(Settings.isIdle, false);
        player.anim.SetBool(Settings.isMoving, false);
        player.anim.SetBool(Settings.isJumping, false);
        player.anim.SetTrigger(Settings.isMeleeAttacking);
    }

    private void SetRangeAttackAnimationParameters()
    {
        player.anim.SetBool(Settings.isIdle, false);
        player.anim.SetBool(Settings.isMoving, false);
        player.anim.SetBool(Settings.isJumping, false);
        player.anim.SetTrigger(Settings.isRangeAttacking);
    }

    private void SetDeathAnimationParameters()
    {
        player.anim.SetBool(Settings.isIdle, false);
        player.anim.SetBool(Settings.isMoving, false);
        player.anim.SetBool(Settings.isJumping, false);
        player.anim.SetBool(Settings.isDead, true);
    }
}
