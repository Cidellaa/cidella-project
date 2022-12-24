using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackEvent : MonoBehaviour
{
    public event Action<RangeAttackEvent, RangeAttackEventArgs> OnRangeAttack;

    public void CallRangeAttackEvent(GameObject prefab, Transform attackPosition, Vector2 direction, float speed)
    {
        OnRangeAttack?.Invoke(this, new RangeAttackEventArgs { prefab = prefab, attackPosition = attackPosition, direction = direction , speed = speed});
    }
}

public class RangeAttackEventArgs : EventArgs
{
    public GameObject prefab;
    public Transform attackPosition;
    public Vector2 direction;
    public float speed;
}
