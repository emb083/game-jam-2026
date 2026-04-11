using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject mainMenuPanel;

    [SerializeField] private GameObject gameplayRoot;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Update()
    {
        if (mainMenuPanel != null && mainMenuPanel.activeSelf)
        {
            ResumeGame();
        }
        else
        {
            OpenPauseMenu();
        }

        if (winPanel != null && winPanel.activeSelf)
        {
            return;
        }

        if (losePanel != null && losePanel.activeSelf)
        {
            return;
        }

        if (controlsPanel != null && controlsPanel.activeSelf)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuPanel != null && pauseMenuPanel.activeSelf)
            {
                pauseMenuPanel.SetActive(true);
            }
            Time.timeScale = 0f;
        }

    }

    public void OpenPauseMenu()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }
        Time.timeScale = 1f;

    }

    public void OpenControls()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        if (controlsPanel != null)
        {
            controlsPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void ReturnToMainMenu()
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        if (gameplayRoot != null)
        {
            controlsPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }
}