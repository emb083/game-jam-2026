using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup loseScreen;
    [SerializeField] private CanvasGroup mainMenuScreen;

    [Header("Buttons")]
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(Retry);
        }

        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }


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

    // Update is called once per frame
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        HideScreen(loseScreen);
        ShowScreen(mainMenuScreen);
        Time.timeScale = 1f;
    }
}