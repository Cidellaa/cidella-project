public enum GameState
{
    GameStarted,
    GamePaused,
    BossFight,
    StartingBossStage,
    GameLost,
    GameWon,
    Dialogue
}

public enum BossState
{
    Dialogue,
    Idle,
    FollowPlayer,
    MeleeAttack,
    RangeAttack,
    BigCandiesAttack,
    CallElves,
    Death
}

public enum EnemyState
{
    Idle,
    Patrol,
    FollowPlayer,
    MeleeAttack,
    RangeAttack,
    Death
}
