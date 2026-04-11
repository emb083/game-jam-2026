using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private CanvasGroup pauseMenuScreen;
    [SerializeField] private CanvasGroup controlsScreen;
    [SerializeField] private CanvasGroup mainMenuScreen;
    [SerializeField] private CanvasGroup gameplayScreen;

    [Header("Buttons")]
    [SerializeField] private Button openPauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button controlButton;
    [SerializeField] private Button mainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Update()
    {
        if (openPauseButton != null)
        {
            openPauseButton.onClick.AddListener(OpenPauseMenu);
        }

        if (resumeButton != null)
        { 
            resumeButton.onClick.AddListener(ResumeGame);
        }

        if (controlButton != null)
        {
            controlButton.onClick.AddListener(OpenControls);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }
            
    private void ShowScreen(CanvasGroup screen)
    {
        if (screen == null)
        {
            return;
        }
        screen.alpha = 1f;
        screen.interactable = true;
        screen.blocksRaycasts = true;
    }

    public void HideScreen(CanvasGroup screen)
    {
        if (screen == null)
        {
            return;
        }
        screen.alpha = 0f;
        screen.interactable = false;
        screen.blocksRaycasts = false;
    } 

    public void OpenPauseMenu()
    {
        if (pauseMenuScreen != null)
        {
            ShowScreen(pauseMenuScreen);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame()
    {
        if (pauseMenuScreen != null)
        {
            HideScreen(pauseMenuScreen);
            Time.timeScale = 1f;
        }
    }

    public void OpenControls()
    {
        if (pauseMenuScreen != null)
        {
            HideScreen(pauseMenuScreen);
            ShowScreen(controlsScreen);
            Time.timeScale = 0f;
        }
    }

    public void ReturnToMainMenu()
    {
        if (pauseMenuScreen != null)
        {
            HideScreen(pauseMenuScreen);
            HideScreen(gameplayScreen);
            ShowScreen(mainMenuScreen);
            Time.timeScale = 1f;
        }
    }
}