using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeScreen_1;
    [SerializeField] private CanvasGroup fadeScreen_2;
    [SerializeField] private RectTransform pausePanel;

    #region Header HEARTS
    [Space(10)]
    [Header("HEARTS")]
    #endregion
    [SerializeField] private Image heart_1;
    [SerializeField] private Image heart_2;
    [SerializeField] private Image heart_3;
    [SerializeField] private Image heart_4;
    [SerializeField] private Image heart_5;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private Sprite emptyHeart;

    private Player player;
    private Boss boss;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
    }

    private void OnEnable()
    {
        player.healthEvent.OnHealthChanged += HealthEvent_OnHealthChanged;
        player.destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyed;
        boss.destroyedEvent.OnDestroyed += DestroyedEvent_OnDestroyedBoss;
    }

    private void OnDisable()
    {
        player.healthEvent.OnHealthChanged -= HealthEvent_OnHealthChanged;
        player.destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyed;
        boss.destroyedEvent.OnDestroyed -= DestroyedEvent_OnDestroyedBoss;
    }

    private void DestroyedEvent_OnDestroyedBoss(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        StartCoroutine(GameWon());
    }

    private void DestroyedEvent_OnDestroyed(DestroyedEvent destroyedEvent, DestroyedEventArgs destroyedEventArgs)
    {
        StartCoroutine(FadeOutScreen());
    }

    private void HealthEvent_OnHealthChanged(HealthEvent healthEvent, HealthEventArgs healthEventArgs)
    {
        switch (player.GetCurrentHealth())
        {
            case 10:
                heart_1.sprite = fullHeart;
                heart_2.sprite = fullHeart;
                heart_3.sprite = fullHeart;
                heart_4.sprite = fullHeart;
                heart_5.sprite = fullHeart;
                break;

            case 9:
                heart_1.sprite = fullHeart;
                heart_2.sprite = fullHeart;
                heart_3.sprite = fullHeart;
                heart_4.sprite = fullHeart;
                heart_5.sprite = halfHeart;
                break;

            case 8:
                heart_1.sprite = fullHeart;
                heart_2.sprite = fullHeart;
                heart_3.sprite = fullHeart;
                heart_4.sprite = fullHeart;
                heart_5.sprite = emptyHeart;
                break;

            case 7:
                heart_1.sprite = fullHeart;
                heart_2.sprite = fullHeart;
                heart_3.sprite = fullHeart;
                heart_4.sprite = halfHeart;
                heart_5.sprite = emptyHeart;
                break;

            case 6:
                heart_1.sprite = fullHeart;
                heart_2.sprite = fullHeart;
                heart_3.sprite = fullHeart;
                heart_4.sprite = emptyHeart;
                heart_5.sprite = emptyHeart;
                break;

            case 5:
                heart_1.sprite = fullHeart;
                heart_2.sprite = fullHeart;
                heart_3.sprite = halfHeart;
                heart_4.sprite = emptyHeart;
                heart_5.sprite = emptyHeart;
                break;

            case 4:
                heart_1.sprite = fullHeart;
                heart_2.sprite = fullHeart;
                heart_3.sprite = emptyHeart;
                heart_4.sprite = emptyHeart;
                heart_5.sprite = emptyHeart;
                break;

            case 3:
                heart_1.sprite = fullHeart;
                heart_2.sprite = halfHeart;
                heart_3.sprite = emptyHeart;
                heart_4.sprite = emptyHeart;
                heart_5.sprite = emptyHeart;
                break;

            case 2:
                heart_1.sprite = fullHeart;
                heart_2.sprite = emptyHeart;
                heart_3.sprite = emptyHeart;
                heart_4.sprite = emptyHeart;
                heart_5.sprite = emptyHeart;
                break;

            case 1:
                heart_1.sprite = halfHeart;
                heart_2.sprite = emptyHeart;
                heart_3.sprite = emptyHeart;
                heart_4.sprite = emptyHeart;
                heart_5.sprite = emptyHeart;
                break;

            case 0:
                heart_1.sprite = emptyHeart;
                heart_2.sprite = emptyHeart;
                heart_3.sprite = emptyHeart;
                heart_4.sprite = emptyHeart;
                heart_5.sprite = emptyHeart;
                break;
        }
    }

    private IEnumerator FadeOutScreen()
    {
        fadeScreen_2.DOFade(1f, 4f);
        GameManager.Instance.GetPlayer().playerController.DisablePlayer();
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator GameWon()
    {
        fadeScreen_2.DOFade(1f, 4f);
        GameManager.Instance.GetPlayer().playerController.DisablePlayer();
        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene("EndingScene");
    }

    #region Buttons' OnClick Methods
    #region PAUSE
    public void Pause()
    {
        StartCoroutine(PauseRoutine());
        Time.timeScale = 0;
    }

    private IEnumerator PauseRoutine()
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1);
        pausePanel.gameObject.SetActive(true);
        float scale = 0;
        while (scale != 1)
        {
            scale += Time.unscaledDeltaTime;
            scale = Mathf.Clamp01(scale);
            fadeScreen_1.blocksRaycasts = true;
            pausePanel.localScale = new Vector3(scale, scale, scale);
            fadeScreen_1.alpha = scale / 2;
            yield return null;
        }
    }
    #endregion

    #region RESUME
    public void Resume()
    {
        Time.timeScale = 1;
        StartCoroutine(ResumeRoutine());
    }

    private IEnumerator ResumeRoutine()
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1);
        pausePanel.DOScale(0f, 1f);
        fadeScreen_1.DOFade(0f, 1f);
        yield return new WaitForSeconds(1f);
        fadeScreen_1.blocksRaycasts = false;
        pausePanel.gameObject.SetActive(false);
    }

    #endregion

    #region RESTART
    public void Restart()
    {
        StartCoroutine(RestartRoutine());
    }

    private IEnumerator RestartRoutine()
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1);
        float scale = 0;
        while (scale != 1)
        {
            scale += Time.unscaledDeltaTime;
            scale = Mathf.Clamp01(scale);
            fadeScreen_2.blocksRaycasts = true;
            fadeScreen_2.alpha = scale;
            yield return null;
        }
        Time.timeScale = 1;
        fadeScreen_2.blocksRaycasts = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region MAIN MENU
    public void ReturnMainMenu()
    {
        StartCoroutine(ReturnMainMenuRoutine());
    }

    private IEnumerator ReturnMainMenuRoutine()
    {
        SoundEffectManager.Instance.PlaySoundEffect(0, 1);
        float scale = 0;
        while (scale != 1)
        {
            scale += Time.unscaledDeltaTime;
            scale = Mathf.Clamp01(scale);
            fadeScreen_2.blocksRaycasts = true;
            fadeScreen_2.alpha = scale;
            yield return null;
        }
        Time.timeScale = 1;
        fadeScreen_2.blocksRaycasts = false;
        SceneManager.LoadScene("MainMenuScene");
    }
    #endregion
    #endregion
}
