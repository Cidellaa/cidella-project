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
    }

    private void OnDisable()
    {
        player.idleEvent.OnIdle -= IdleEvent_OnIdle;
        player.movementByVelocityEvent.OnMovementByVelocity -= MovementByVelocityEvent_OnMovementByVelocity;
        player.movementByForceEvent.OnMovementByForce -= MovementByForceEvent_OnMovementByForce;
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

    private void SetIdleAnimationParameters()
    {
        if (!player.OnGround()) return;

        player.anim.SetBool(Settings.isMoving, false);
        player.anim.SetBool(Settings.isJumping, false);
        player.anim.SetBool(Settings.isIdle, true);
    }

    private void SetMovementAnimationParameters()
    {
        if (!player.OnGround()) return;

        player.anim.SetBool(Settings.isIdle, false);
        player.anim.SetBool(Settings.isJumping, false);
        player.anim.SetBool(Settings.isMoving, true);
    }

    private void SetMovementByForceAnimationParameters()
    {
        player.anim.SetBool(Settings.isIdle, true);
        player.anim.SetBool(Settings.isMoving, false);
        player.anim.SetBool(Settings.isJumping, true);
    }
}
