using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    #region ANIMATION PARAMETERS
    #region Player & Boss
    public static int isIdle = Animator.StringToHash("isIdle");
    public static int isMoving = Animator.StringToHash("isMoving");
    public static int isJumping = Animator.StringToHash("isJumping");
    public static int isMeleeAttacking = Animator.StringToHash("isMeleeAttacking");
    public static int isRangeAttacking = Animator.StringToHash("isRangeAttacking");
    public static int isDead = Animator.StringToHash("isDead");
    public static int isCallingSomething = Animator.StringToHash("isCallingSomething");
    #endregion
    #endregion
}
