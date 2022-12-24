public enum GameState
{
    GameStarted,
    GamePaused,
    BossStage,
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
    Walk,
    Attack,
    Death
}
