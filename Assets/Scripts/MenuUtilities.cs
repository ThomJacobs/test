using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MenuUtilities : MonoBehaviour
{
    #region
    [SerializeField] private AudioClip onClickSoundEffect;
    private AudioSource audioSource;
    public CanvasGroup helpMenu;
    public CanvasGroup mainMenu;
    #endregion

    #region Methods
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    public IEnumerator LoadSceneDelay(string sceneName, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Unfocus() => UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null, null);

    public void ReloadScene()
    {
        LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadScene(string sceneName)
    {
        if(audioSource && onClickSoundEffect)
        {
            audioSource.PlayOneShot(onClickSoundEffect);
            StartCoroutine(LoadSceneDelay(sceneName, onClickSoundEffect.length));
            return;
        }

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void LoadSceneAsync(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneDelay(sceneName, delay));
    }

    public void Quit() => Application.Quit();

    public void PauseGame(GameObject gameObject)
    {
        switch(gameObject.activeSelf)
        {
            case false:
                Time.timeScale = 1.0f;
                gameObject.SetActive(true);
                return;

            default:
                Time.timeScale = 0.0f;
                gameObject.SetActive(false);
                return;
        }
    }

    public void Help()
    {
        helpMenu.alpha = 1;
        helpMenu.interactable = true;
        helpMenu.blocksRaycasts = true;

        mainMenu.alpha = 0;
        mainMenu.interactable = false;
        mainMenu.blocksRaycasts = false;
    }

    public void Menu()
    {
        mainMenu.alpha = 1;
        mainMenu.interactable = true;
        mainMenu.blocksRaycasts = true;

        helpMenu.alpha = 0;
        helpMenu.interactable = false;
        helpMenu.blocksRaycasts = false;
    }

    public void Pause(GameObject gameObject)
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(false);
    }

    public void Unpause(GameObject gameObject)
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(true);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Toggle(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }
    #endregion
}
