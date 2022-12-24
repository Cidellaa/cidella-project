using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region ANIMATION PARAMETERS
    #region Player
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isMoving = Animator.StringToHash("isMoving");
    public static int isJumping = Animator.StringToHash("isJumping");
    public static int isAttacking = Animator.StringToHash("isAttacking");
    public static int isDead = Animator.StringToHash("isDead");
    #endregion
    #endregion
}
