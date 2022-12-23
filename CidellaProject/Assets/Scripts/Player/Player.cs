using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementByVelocity))]
[RequireComponent(typeof(MovementByVelocityEvent))]
[RequireComponent(typeof(Idle))]
[RequireComponent(typeof(IdleEvent))]
[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(Animator))]
[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [HideInInspector] public MovementByVelocityEvent movementByVelocityEvent;
    [HideInInspector] public IdleEvent idleEvent;
}
