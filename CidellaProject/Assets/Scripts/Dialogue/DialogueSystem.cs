using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Specialized;

public class DialogueSystem : MonoBehaviour
{
    public List<DialogueLineSO> dialogues = new();

    [SerializeField] private RectTransform dialoguePanel;

    [SerializeField] private Image speakerImage;
    [SerializeField] private TextMeshProUGUI speakerName;
    [SerializeField] private TextMeshProUGUI speakerText;

    private readonly float dialoguePanelTimer = 1f;
    private int lineIndex = 0;

    private bool isLineFinished;
    private bool isTyping;
    public bool isDialogueStarted;

    private void Update()
    {
        if (!isTyping && GameManager.Instance.gameState == GameState.Dialogue)
        {
            if (isDialogueStarted && isLineFinished)
            {
                NextLine();
            }
            else if (!isDialogueStarted)
            {
                StartCoroutine(ShowDialogue());
            }
        }
    }

    private IEnumerator ShowDialogue()
    {
        isDialogueStarted = true;
        dialoguePanel.gameObject.SetActive(true);
        SetSpeaker();
        yield return new WaitForSeconds(1.5f);
        dialoguePanel.DOMoveY(0, dialoguePanelTimer);
        yield return new WaitForSeconds(dialoguePanelTimer);
        StartCoroutine(TypeSentence());
    }

    private IEnumerator TypeSentence()
    {
        isLineFinished = false;
        isTyping = true;
        string dialogueLine = dialogues[lineIndex].text;
        foreach (char letter in dialogueLine.ToCharArray())
        {
            speakerText.text += letter;
            yield return new WaitForSeconds(.075f);
        }
        yield return new WaitForSeconds(1f);
        isLineFinished = true;
        isTyping = false;
    }

    private void NextLine()
    {
        if(lineIndex < dialogues.Count - 1)
        {
            lineIndex++;
            SetSpeaker();
            StartCoroutine(TypeSentence());
        }
        else
        {
            StartCoroutine(FinishDialogue());
        }
    }

    private void SetSpeaker()
    {
        speakerImage.sprite = dialogues[lineIndex].speakerSprite;
        speakerName.text = dialogues[lineIndex].speaker;
        speakerText.text = "";
    }

    private IEnumerator FinishDialogue()
    {
        lineIndex = 0;
        if (GameManager.Instance.GetPlayer().playerController.isBossFightTriggered)
        {
            GameManager.Instance.previousGameState = GameManager.Instance.gameState;
            GameManager.Instance.gameState = GameState.BossFight;
            GameManager.Instance.GetBoss().bossAI.EnableBoss();
            GameManager.Instance.GetPlayer().playerController.EnablePlayer();
        }
        isDialogueStarted = false;
        dialoguePanel.DOMoveY(-350f, dialoguePanelTimer);
        yield return new WaitForSeconds(dialoguePanelTimer);
        dialoguePanel.gameObject.SetActive(false);
    }
}
