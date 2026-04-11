using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup winScreen;
    [SerializeField] private CanvasGroup mainMenuScreen;

    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Start()
    {
        if (playAgainButton != null)
        {
            playAgainButton.onClick.AddListener(PlayAgain);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    public void ShowScreen(CanvasGroup screen)
    {
       if (screen = null)
        {
           screen.alpha = 1f;
           screen.interactable = true;
           screen.blocksRaycasts = true;
        }
    }

    public void HideScreen(CanvasGroup screen)
    {
        if (screen = null)
        {
            screen.alpha = 0f;
            screen.interactable = false;
            screen.blocksRaycasts = false;
        }
    }
    

    public void PlayAgain()
    {         Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    public void ReturnToMainMenu()
    {
        HideScreen(winScreen);
        ShowScreen(mainMenuScreen);
         Time.timeScale = 1f;
    }
}