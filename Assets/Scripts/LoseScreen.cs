using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject mainMenuScreen;

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
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}