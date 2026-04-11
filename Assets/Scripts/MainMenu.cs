using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private CanvasGroup mainMenuScreen;
    [SerializeField] private CanvasGroup controlsScreen;
    [SerializeField] private CanvasGroup gameplayScreen;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGame);
        }

        if (controlsButton != null)
        {
            controlsButton.onClick.AddListener(OpenControls);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    // Update is called once per frame
    public void ShowScreen(CanvasGroup screen)
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

    public void StartGame()
    {
        HideScreen(mainMenuScreen);
        ShowScreen(gameplayScreen);
        Time.timeScale = 1f;
    }

    public void OpenControls()
    {
        HideScreen(mainMenuScreen);
        ShowScreen(controlsScreen);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}