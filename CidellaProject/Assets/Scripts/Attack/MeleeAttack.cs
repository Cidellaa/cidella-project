using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeleeAttackEvent))]
[DisallowMultipleComponent]
public class MeleeAttack : MonoBehaviour
{
    private MeleeAttackEvent meleeAttackEvent;

    private void Awake()
    {
        meleeAttackEvent = GetComponent<MeleeAttackEvent>();
    }

    private void OnEnable()
    {
        meleeAttackEvent.OnMeleeAttack += MeleeAttackEvent_OnMeleeAttack;
    }

    private void OnDisable()
    {
        meleeAttackEvent.OnMeleeAttack -= MeleeAttackEvent_OnMeleeAttack;
    }

    private void MeleeAttackEvent_OnMeleeAttack(MeleeAttackEvent meleeAttackEvent, MeleeAttackEventArgs meleeAttackEventArgs)
    {
        Attack(meleeAttackEventArgs.attackPosition, meleeAttackEventArgs.attackRadius, meleeAttackEventArgs.whatIsEnemy);
    }

    private void Attack(Transform attackPosition, float attackRadius, LayerMask whatIsEnemy)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, whatIsEnemy);

        foreach (Collider2D enemy in hitEnemies)
        {
            if(enemy.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(1);
            }
        }
    }
}
