using System;
using UnityEngine;

public class MovementByVelocityEvent : MonoBehaviour
{
    public event Action<MovementByVelocityEvent, MovementByVelocityEventArgs> OnMovementByVelocity;
    
    public void CallMovementByVelocityEvent(float speed, float direction)
    {
        OnMovementByVelocity?.Invoke(this, new MovementByVelocityEventArgs { speed = speed, direction = direction });
    }
}

public class MovementByVelocityEventArgs : EventArgs
{
    public float speed;
    public float direction;
}
