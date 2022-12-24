using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementToPositionEvent : MonoBehaviour
{
    public event Action<MovementToPositionEvent, MovementToPositionEventArgs> OnMovementToPosition;

    public void CallMovementToPositionEvent(float moveSpeed, Vector3 currentPosition, Vector3 targetPosition)
    {
        OnMovementToPosition?.Invoke(this, new MovementToPositionEventArgs { moveSpeed = moveSpeed, currentPosition = currentPosition, targetPosition = targetPosition });
    }
}

public class MovementToPositionEventArgs : EventArgs
{
    public float moveSpeed;
    public Vector3 currentPosition;
    public Vector3 targetPosition;
}
