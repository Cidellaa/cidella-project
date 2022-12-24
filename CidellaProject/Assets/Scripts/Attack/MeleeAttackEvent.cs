using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackEvent : MonoBehaviour
{
    public event Action<MeleeAttackEvent, MeleeAttackEventArgs> OnMeleeAttack;

    public void CallMeleeAttack(Transform attackPosition, float attackRadius, LayerMask whatIsEnemy)
    {
        OnMeleeAttack?.Invoke(this, new MeleeAttackEventArgs { attackPosition = attackPosition, attackRadius = attackRadius, whatIsEnemy = whatIsEnemy});
    }
}

public class MeleeAttackEventArgs : EventArgs
{
    public Transform attackPosition;
    public float attackRadius;
    public LayerMask whatIsEnemy;
}
