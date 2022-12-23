using System;
using UnityEngine;

[DisallowMultipleComponent]
public class MovementByForceEvent : MonoBehaviour
{
    public event Action<MovementByForceEvent, MovementByForceEventArgs> OnMovementByForce;

    public void CallMovementByForceEvent(Vector2 direction, float force)
    {
        OnMovementByForce?.Invoke(this, new MovementByForceEventArgs { direction = direction, force = force });
    }
}

public class MovementByForceEventArgs : EventArgs
{
    public Vector2 direction;
    public float force;
}