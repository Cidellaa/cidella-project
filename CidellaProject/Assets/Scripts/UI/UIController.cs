using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeScreen_1;
    [SerializeField] private CanvasGroup fadeScreen_2;
    [SerializeField] private RectTransform pausePanel;

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
