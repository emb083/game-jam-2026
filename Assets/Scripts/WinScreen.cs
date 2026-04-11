using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameplayRoot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    public void ReturnToMainMenu()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        Time.timeScale = 1f;
    }
}