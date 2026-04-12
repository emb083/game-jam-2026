using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject controlsScreen;
    [SerializeField] private GameObject gameplayScreen;

    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        Time.timeScale = 0f;
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
    public void ShowScreen(GameObject screen)
    {
        if (screen == null)
        {
            return;
        }

        screen.SetActive(true);
    }

    public void HideScreen(GameObject screen)
    {
        if (screen == null)
        {
            return;
        }

        screen.SetActive(false);
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
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}