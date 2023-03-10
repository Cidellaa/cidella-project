using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    #region Header BUTTONS
    [Space(10)]
    [Header("BUTTONS")]
    #endregion
    [SerializeField] private RectTransform volumeButton;
    [SerializeField] private RectTransform playButton;
    [SerializeField] private RectTransform gameplayButton;
    [SerializeField] private RectTransform creditsButton;
    [SerializeField] private RectTransform quitButton;

    #region Header PANELS
    [Space(10)]
    [Header("PANELS")]
    #endregion
    [SerializeField] private RectTransform gameplayPanel;
    [SerializeField] private RectTransform creditsPanel;

    [Space(10)]
    [SerializeField] private CanvasGroup fadeScreen;
    [SerializeField] private CanvasGroup gameName;
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private Sprite volumeOffSprite;
    [SerializeField] private Sprite volumeOnSprite;

    #region Timer Variables
    private readonly float buttonOpeningTimer = .6f;
    private readonly float fadeTimer = 2f;
    private readonly float panelTimer = 2f;
    #endregion

    private void Start()
    {
        StartCoroutine(StartUI());
    }

    private IEnumerator StartUI()
    {
        fadeScreen.DOFade(0f, fadeTimer);
        yield return new WaitForSeconds(fadeTimer);

        gameName.DOFade(1f, fadeTimer);
        yield return new WaitForSeconds(fadeTimer);
        
        playButton.DOScale(5f, buttonOpeningTimer).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(buttonOpeningTimer);
        
        gameplayButton.DOScale(5f, buttonOpeningTimer).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(buttonOpeningTimer);
        
        creditsButton.DOScale(5f, buttonOpeningTimer).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(buttonOpeningTimer);
        
        quitButton.DOScale(5f, buttonOpeningTimer).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(buttonOpeningTimer);

        volumeButton.DOScale(1f, buttonOpeningTimer).SetEase(Ease.InQuint);
        yield return new WaitForSeconds(buttonOpeningTimer);

        fadeScreen.blocksRaycasts = false;
    }

    #region Buttons' OnClick Methods

    #region Play Button
    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1f);
        fadeScreen.blocksRaycasts = true;
        fadeScreen.DOFade(1f, fadeTimer);
        yield return new WaitForSeconds(fadeTimer);
        
        SceneManager.LoadScene("MainGameScene");
    }
    #endregion

    #region Gameplay Button
    public void Gameplay()
    {
        StartCoroutine(OpenPanelRoutine(gameplayPanel));
    }
    #endregion

    #region Credits Button
    public void Credits()
    {
        StartCoroutine(OpenPanelRoutine(creditsPanel));
    }
    #endregion

    #region Quit Button
    public void Quit()
    {
        StartCoroutine(QuitRoutine());
    }

    private IEnumerator QuitRoutine()
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1f);
        fadeScreen.blocksRaycasts = true;
        fadeScreen.DOFade(1f, fadeTimer);
        yield return new WaitForSeconds(fadeTimer);

        if (EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
    #endregion

    #region Back Button
    public void Back(RectTransform panel)
    {
        StartCoroutine(BackRoutine(panel));
    }

    private IEnumerator BackRoutine(RectTransform panel)
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1f);
        panel.DOMoveY(2000f, panelTimer);
        fadeScreen.DOFade(0f, panelTimer);
        yield return new WaitForSeconds(panelTimer);
        panel.gameObject.SetActive(false);
        fadeScreen.blocksRaycasts = false;
    }
    #endregion

    #region Volume Button
    public void VolumeOnOff()
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1f);
        if (volumeButton.GetComponent<Image>().sprite == volumeOnSprite)
        {
            //menuMusic.volume = 0;
            volumeButton.GetComponent<Image>().sprite = volumeOffSprite;
        }
        else if (volumeButton.GetComponent<Image>().sprite == volumeOffSprite)
        {
            //menuMusic.volume = 1;
            volumeButton.GetComponent<Image>().sprite = volumeOnSprite;
        }
    }
    #endregion

    #region Panel
    private IEnumerator OpenPanelRoutine(RectTransform panel)
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1f);
        panel.gameObject.SetActive(true);
        yield return null;
        
        fadeScreen.blocksRaycasts = true;
        fadeScreen.DOFade(.5f, panelTimer);
        panel.DOMoveY(655f, panelTimer).SetEase(Ease.OutQuint);
    }
    #endregion

    #endregion
}
