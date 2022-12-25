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

    #region Header RANGE ATTACK
    [Space(10)]
    [Header("RANGE ATTACK")]
    #endregion
    [SerializeField] private GameObject rangeWeaponPrefab;
    [SerializeField] private Transform rangeAttackPosition;
    [SerializeField] private float rangeWeaponSpeed = 13f;
    private float rangeAttackTimer = 3f;

    #region Header MELEE ATTACK
    [Space(10)]
    [Header("MELEE ATTACK")]
    #endregion
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private LayerMask whatIsEnemy;

    #region Header MOVEMENT
    [Space(10)]
    [Header("MOVEMENT")]
    #endregion
    [SerializeField] private float moveSpeed = 15f;

    private bool isAttacking;
    private bool isBossDisabled;

    private Transform player;
    [SerializeField] private BossState bossState = BossState.Idle;
    private Boss boss;
    private Rigidbody2D rb;
    private Collider2D col;


    private void Awake()
    {
        boss = GetComponent<Boss>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        isBossDisabled = true;
        candiesPlaces.parent = null;
        elvesPlaces.parent = null;
    }

    private void Update()
    {
        if (isBossDisabled) return;
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
                if (CheckAttacks()) break;
                FollowPlayer();
                break;

            case BossState.MeleeAttack:
                if (!isAttacking) StartCoroutine(MeleeAttackRoutine());
                break;

            case BossState.RangeAttack:
                if (!isAttacking) StartCoroutine(RangeAttackRoutine());
                break;

            case BossState.BigCandiesAttack:
                if (!isAttacking) StartCoroutine(BigCandiesAttackRoutine());
                break;

            case BossState.CallElves:
                if (!isAttacking) StartCoroutine(CallElvesRoutine());
                break;

            case BossState.Death:
                DisableBoss();
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

    private IEnumerator MeleeAttackRoutine()
    {
        isAttacking = true;
        boss.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.75f);
        boss.meleeAttackEvent.CallMeleeAttack(meleeAttackPosition, meleeAttackRadius, whatIsEnemy);
        yield return new WaitForSeconds(.6f);
        bossState = BossState.Idle;
        isAttacking = false;
    }

    private IEnumerator RangeAttackRoutine()
    {
        isAttacking = true;
        boss.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.75f);
        boss.rangeAttackEvent.CallRangeAttackEvent(rangeWeaponPrefab, rangeAttackPosition, transform.localScale, rangeWeaponSpeed);
        yield return new WaitForSeconds(.7f);
        bossState = BossState.Idle;
        isAttacking = false;
    }

    private IEnumerator BigCandiesAttackRoutine()
    {
        isAttacking = true;
        boss.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.75f);
        boss.callSomethingEvent.CallCallSomethingEvent(candyPrefab, candiesPlaces);
        yield return new WaitForSeconds(1.125f);
        bossState = BossState.Idle;
        isAttacking = false;
    }

    private IEnumerator CallElvesRoutine()
    {
        isAttacking = true;
        boss.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.75f);
        boss.callSomethingEvent.CallCallSomethingEvent(elfPrefab, elvesPlaces);
        yield return new WaitForSeconds(1.125f);
        bossState = BossState.Idle;
        isAttacking = false;
    }

    #region Attack Checks
    private bool CheckAttacks()
    {
        if (CheckRangeAttack())
        {
            bossState = BossState.RangeAttack;
            rangeAttackTimer = Random.Range(1.5f, 3.5f);
            return true;
        }
        else if (CheckBigCandiesAttack())
        {
            bossState = BossState.BigCandiesAttack;
            bigCandyAttackTimer = Random.Range(5f, 7f);
            return true;
        }
        else if (CheckCallElves())
        {
            bossState = BossState.CallElves;
            callElvesTimer = Random.Range(7f, 10f);
            return true;
        }
        return false;
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

    public void DisableBoss()
    {
        isBossDisabled = true;
        rb.gravityScale = 0f;
        col.enabled = false;
    }

    public void EnableBoss()
    {
        isBossDisabled = false;
        rb.gravityScale = 1f;
        col.enabled = true;
    }
}
