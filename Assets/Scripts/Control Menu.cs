using UnityEngine;
using UnityEngine.Rendering;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;

    [SerializeField] private bool openedFromPauseMenu = false;

    public void Back()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(false);
        }

        if (openedFromPauseMenu != null)
        {
            if (pauseMenuPanel != null)
            {
                pauseMenuPanel.SetActive(true);
            }
            Time.timeScale = 0f;
        }
        else
            {
                if (mainMenuPanel != null)
                {
                    mainMenuPanel.SetActive(true);
                }
            Time.timeScale = 1f;
        }
    }
}