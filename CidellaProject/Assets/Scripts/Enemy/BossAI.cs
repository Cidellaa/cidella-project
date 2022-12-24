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
    [SerializeField] private float callElvesTimer = 7f;

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
    private float rangeAttackTimer = 3f;

    #region Header MOVEMENT
    [Space(10)]
    [Header("MOVEMENT")]
    #endregion
    [SerializeField] private float moveSpeed = 15f;

    private Boss boss;
    [SerializeField] private BossState bossState = BossState.Idle; // DELETE SERIALIZEFIELD LATER!!
    private Transform player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = GetComponent<Boss>();
    }

    private void Update()
    {
        HandleBossState();
    }

    private void HandleBossState()
    {
        switch (bossState)
        {
            case BossState.Dialogue:
                boss.idleEvent.CallIdleEvent();
                break;

            case BossState.Idle:
                boss.idleEvent.CallIdleEvent();

                if (ShouldFollowPlayer())
                    bossState = BossState.FollowPlayer;
                else
                    CheckAttacks();
                break;

            case BossState.FollowPlayer:
                FollowPlayer();
                CheckAttacks();
                break;

            case BossState.MeleeAttack:
                
                break;

            case BossState.RangeAttack:
                StartCoroutine(RangeAttackRoutine());
                break;
            
            case BossState.BigCandiesAttack:
                StartCoroutine(BigCandiesAttackRoutine());
                break;
            
            case BossState.CallElves:
                StartCoroutine(CallElvesRoutine());
                break;
            
            case BossState.Death:
            
                break;
        }
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) > .8f)
        {
            boss.movementToPositionEvent.CallMovementToPositionEvent(moveSpeed, transform.position, player.position);
        }
        else
            bossState = BossState.MeleeAttack;
    }

    private bool ShouldFollowPlayer()
    {
        return Vector3.Distance(player.position, transform.position) > .8f;
    }

    private IEnumerator RangeAttackRoutine()
    {
        bossState = BossState.Idle;
        boss.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.5f);
        boss.rangeAttackEvent.CallRangeAttackEvent(rangeWeaponPrefab, rangeAttackPosition, transform.localScale, rangeWeaponSpeed);
        yield return new WaitForSeconds(.7f);
    }

    private IEnumerator BigCandiesAttackRoutine()
    {
        bossState = BossState.Idle;
        boss.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.5f);
        boss.callSomethingEvent.CallCallSomethingEvent(candyPrefab, candiesPlaces);
        yield return new WaitForSeconds(1.125f);
    }

    private IEnumerator CallElvesRoutine()
    {
        bossState = BossState.Idle;
        boss.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.5f);
        boss.callSomethingEvent.CallCallSomethingEvent(elfPrefab, elvesPlaces);
        yield return new WaitForSeconds(1.125f);
    }

    #region Attack Checks
    private void CheckAttacks()
    {
        if (CheckRangeAttack())
        {
            bossState = BossState.RangeAttack;
            rangeAttackTimer = Random.Range(1.5f, 3.5f);
        }
        else if (CheckBigCandiesAttack())
        {
            bossState = BossState.BigCandiesAttack;
            bigCandyAttackTimer = Random.Range(7f, 13f);
        }
        else if (CheckCallElves())
        {
            bossState = BossState.CallElves;
            callElvesTimer = Random.Range(7f, 13f);
        }
    }

    private bool CheckRangeAttack()
    {
        rangeAttackTimer -= Time.deltaTime;
        return rangeAttackTimer <= 0f;
    }

    private bool CheckBigCandiesAttack()
    {
        bigCandyAttackTimer -= Time.deltaTime;

        return bigCandyAttackTimer <= 0;
    }

    private bool CheckCallElves()
    {
        callElvesTimer -= Time.deltaTime;

        return callElvesTimer <= 0;
    }
    #endregion
}
