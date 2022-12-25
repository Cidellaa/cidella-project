using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : StaticMonoBehaviour<GameManager>
{
    private Player player;
    private Boss boss;
    private DialogueSystem dialogueSystem;

    public GameState gameState = GameState.GameStarted;
    public GameState previousGameState = GameState.GameStarted;

    private bool isDialogueStarted;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        dialogueSystem = GetComponent<DialogueSystem>();
    }

    private void Update()
    {
        HandleGameState();
    }

    private void HandleGameState()
    {
        switch (gameState)
        {
            case GameState.GameStarted:
                if (GetPlayer().playerController.isBossFightTriggered) gameState = GameState.StartingBossStage;
                break;

            case GameState.StartingBossStage:
                if (Vector3.Distance(player.gameObject.transform.position, boss.transform.GetChild(0).position) > .5f)
                {
                    GetPlayer().playerController.DisablePlayer();
                    GetPlayer().movementToPositionEvent.CallMovementToPositionEvent(3f, GetPlayer().transform.position, boss.transform.GetChild(0).position);
                }
                else
                {
                    GetPlayer().idleEvent.CallIdleEvent();
                    gameState = GameState.Dialogue;
                }
                break;

            case GameState.BossFight:

                break;

            case GameState.Dialogue:
                GetPlayer().playerController.DisablePlayer();
                boss.bossAI.DisableBoss();
                if (!dialogueSystem.isDialogueStarted) dialogueSystem.isDialogueStarted = true;
                break;
        }
    }

    public Player GetPlayer()
    {
        return player;
    }
}
