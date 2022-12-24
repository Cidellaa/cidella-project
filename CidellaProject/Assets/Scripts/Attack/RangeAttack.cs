using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangeAttackEvent))]
[DisallowMultipleComponent]
public class RangeAttack : MonoBehaviour
{
    private RangeAttackEvent rangeAttackEvent;

    private void Awake()
    {
        rangeAttackEvent = GetComponent<RangeAttackEvent>();
    }

    private void OnEnable()
    {
        rangeAttackEvent.OnRangeAttack += RangeAttackEvent_OnRangeAttack;
    }

    private void OnDisable()
    {
        rangeAttackEvent.OnRangeAttack -= RangeAttackEvent_OnRangeAttack;
    }

    private void RangeAttackEvent_OnRangeAttack(RangeAttackEvent rangeAttackEvent, RangeAttackEventArgs rangeAttackEventArgs)
    {
        InstantiateObject(rangeAttackEventArgs.prefab, rangeAttackEventArgs.attackPosition, rangeAttackEventArgs.direction, rangeAttackEventArgs.speed);
    }

    private void InstantiateObject(GameObject prefab, Transform attackPosition, Vector2 direction, float speed)
    {
        GameObject go = Instantiate(prefab, attackPosition.position, Quaternion.identity);
        go.GetComponent<Rigidbody2D>().velocity = new(transform.localScale.x * speed, 0f);
    }
}
