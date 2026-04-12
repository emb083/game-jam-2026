using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [Header("Screens")]
    [SerializeField] private GameObject pauseMenuScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private GameObject MainMenuScreen;

    [Header("Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    public static PauseMenu Instance {get; private set;}

    private void Awake(){
        Instance = this;
    }

    public void Update()
    {
        if (resumeButton != null)
        { 
            resumeButton.onClick.AddListener(ResumeGame);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }
            
    private void ShowScreen(GameObject screen)
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
            ShowScreen(gameplayScreen);
            Time.timeScale = 1f;
        }
    }

    public void OpenControls()
    {
        if (pauseMenuScreen != null)
        {
            HideScreen(pauseMenuScreen);
            Time.timeScale = 0f;
        }
    }

    public void ReturnToMainMenu()
    {
        if (pauseMenuScreen != null)
        {
            HideScreen(pauseMenuScreen);
            HideScreen(gameplayScreen);
            ShowScreen(MainMenuScreen);
            Time.timeScale = 0f;
        }
    }
}