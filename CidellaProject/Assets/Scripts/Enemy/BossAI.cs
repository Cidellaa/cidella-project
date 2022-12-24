using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    #region Header ELVES
    [Space(5)]
    [Header("ELVES")]
    #endregion
    [SerializeField] private GameObject elfPrefab;
    [SerializeField] private Transform elvesPlaces;
    private float elvesCallTimer = 10f;

    #region Header CANDIES
    [Space(10)]
    [Header("CANDIES")]
    #endregion
    [SerializeField] private GameObject candyPrefab;
    [SerializeField] private Transform candiesPlaces;
    private float bigCandyAttackTimer = 10f;

    #region RANGE ATTACK
    [Space(10)]
    [Header("RANGE ATTACK")]
    #endregion
    [SerializeField] private GameObject rangeWeaponPrefab;
    [SerializeField] private Transform rangeAttackPosition;
    [SerializeField] private float rangeWeaponSpeed = 13f;

    private Boss boss;
    [SerializeField] private BossState bossState = BossState.Idle; // DELETE SERIALIZEFIELD LATER!!

    private void Awake()
    {
        boss = GetComponent<Boss>();
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        switch (bossState)
        {
            case BossState.Idle:
                boss.idleEvent.CallIdleEvent();
                break;
            
            case BossState.RangeAttack:
                boss.rangeAttackEvent.CallRangeAttackEvent(rangeWeaponPrefab, rangeAttackPosition, transform.localScale, rangeWeaponSpeed);
                break;
            
            case BossState.BigCandiesAttack:
                boss.callSomethingEvent.CallCallSomethingEvent(candyPrefab, candiesPlaces);
                bossState = BossState.Idle;
                break;
            
            case BossState.CallElves:
                boss.callSomethingEvent.CallCallSomethingEvent(elfPrefab, elvesPlaces);
                bossState = BossState.Idle;
                break;
            
            case BossState.Death:
            
                break;
        }
    }
}
