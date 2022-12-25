using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]
public class EnemyAI : MonoBehaviour
{

    #region Header MOVEMENT
    [Space(5)]
    [Header("MOVEMENT")]
    #endregion
    [SerializeField] private float moveSpeed;

    #region Header MELEE ATTACK
    [Space(5)]
    [Header("MELEE ATTACK")]
    #endregion 
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private float meleeAttackRadius;
    [SerializeField] private LayerMask whatIsEnemy;

    #region Header RANGE ATTACK
    [Space(5)]
    [Header("RANGE ATTACK")]
    #endregion 
    [SerializeField] private Transform rangeAttackPosition;
    [SerializeField] private GameObject rangeWeaponPrefab;
    [SerializeField] private float rangeWeaponSpeed;
    private float rangeAttackTimer = 3f;

    [Space(10)]
    [SerializeField] private Transform leftTarget;
    [SerializeField] private Transform rightTarget;

    private bool isEnemyDisabled;
    private bool isAttacking;
    private Transform player;
    [SerializeField] private EnemyState enemyState = EnemyState.Patrol; 

    private Enemy enemy;
    private Collider2D col;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        moveSpeed = Random.Range(1.5f, 3f);
        leftTarget.parent = null;
        rightTarget.parent = null;
    }

    private void Update()
    {
        if (isEnemyDisabled) return;
        HandleEnemyState();
    }

    private void HandleEnemyState()
    {
        switch(enemyState)
        {
            case EnemyState.Idle:
                if (ShouldFollowPlayer())
                    enemyState = EnemyState.FollowPlayer;
                else
                    CheckAttacks();
                break;

            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.FollowPlayer:
                if (CheckAttacks()) break;
                FollowPlayer();
                break;

            case EnemyState.MeleeAttack:
                if (!isAttacking) StartCoroutine(MeleeAttackRoutine());
                break;

            case EnemyState.RangeAttack:
                if (!isAttacking) StartCoroutine(RangeAttackRoutine());
                break;

            case EnemyState.Death:
                DisableEnemy();
                break;
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(player.position, transform.position) <= 20f) enemyState = EnemyState.FollowPlayer;

        if (Vector3.Distance(leftTarget.position, transform.position) > .1f)
            enemy.movementToPositionEvent.CallMovementToPositionEvent(moveSpeed, transform.position, leftTarget.position);
        else
            enemy.movementToPositionEvent.CallMovementToPositionEvent(moveSpeed, transform.position, rightTarget.position);
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(player.position, transform.position) > .8f)
        {
            enemy.movementToPositionEvent.CallMovementToPositionEvent(moveSpeed, transform.position, player.position);
        }
        else
            enemyState = EnemyState.MeleeAttack;
    }

    private bool ShouldFollowPlayer()
    {
        return Vector3.Distance(player.position, transform.position) > .8f;
    }

    private IEnumerator MeleeAttackRoutine()
    {
        isAttacking = true;
        enemy.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.75f);
        enemy.meleeAttackEvent.CallMeleeAttack(meleeAttackPosition, meleeAttackRadius, whatIsEnemy);
        yield return new WaitForSeconds(.6f);
        enemyState = EnemyState.Idle;
        isAttacking = false;
    }

    private IEnumerator RangeAttackRoutine()
    {
        isAttacking = true;
        enemy.idleEvent.CallIdleEvent();
        yield return new WaitForSeconds(.75f);
        enemy.rangeAttackEvent.CallRangeAttackEvent(rangeWeaponPrefab, rangeAttackPosition, transform.localScale, rangeWeaponSpeed);
        yield return new WaitForSeconds(.7f);
        enemyState = EnemyState.Idle;
        isAttacking = false;
    }

    #region Attack Checks
    private bool CheckAttacks()
    {
        if (CheckRangeAttack())
        {
            enemyState = EnemyState.RangeAttack;
            rangeAttackTimer = Random.Range(1.5f, 3.5f);
            return true;
        }
        return false;
    }

    private bool CheckRangeAttack()
    {
        rangeAttackTimer -= Time.deltaTime;

        return rangeAttackTimer <= 0f;
    }
    #endregion

    public void DisableEnemy()
    {
        isEnemyDisabled = true;
        rb.gravityScale = 0;
        col.enabled = false;
    }

}
