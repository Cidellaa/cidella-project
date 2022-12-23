using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (horizontalMovement != 0)
        {
            player.movementByVelocityEvent.CallMovementByVelocityEvent(moveSpeed, horizontalMovement);
        }
        else
            player.idleEvent.CallIdleEvent();
    }

}
