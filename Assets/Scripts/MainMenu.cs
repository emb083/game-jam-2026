using UnityEngine;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject gameplayRoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (gameplayRoot != null)
        {
            gameplayRoot.SetActive(true);
        }

        Time.timeScale = 1f;
    }

    // Update is called once per frame
    public void OpenControls()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (controlsPanel != null)
        {
            controlsPanel.SetActive(true);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}