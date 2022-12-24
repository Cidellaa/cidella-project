using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : StaticMonoBehaviour<GameManager>
{
    private Player player;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public Player GetPlayer()
    {
        return player;
    }
}
