using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeScreen;
    [SerializeField] private CanvasGroup gameName;
    [SerializeField] private Button playButton;
    [SerializeField] private Button gameplayButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    private float buttonOpeningTimer = .5f;

    private void Start()
    {
        StartCoroutine(StartUI());
    }

    private IEnumerator StartUI()
    {
        fadeScreen.DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
        gameName.DOFade(1f, 2f);
        yield return new WaitForSeconds(2f);
        playButton.GetComponent<RectTransform>().DOScale(1f, buttonOpeningTimer);
        yield return new WaitForSeconds(buttonOpeningTimer);
        gameplayButton.GetComponent<RectTransform>().DOScale(1f, buttonOpeningTimer);
        yield return new WaitForSeconds(buttonOpeningTimer);
        settingsButton.GetComponent<RectTransform>().DOScale(1f, buttonOpeningTimer);
        yield return new WaitForSeconds(buttonOpeningTimer);
        quitButton.GetComponent<RectTransform>().DOScale(1f, buttonOpeningTimer);
    }
}
