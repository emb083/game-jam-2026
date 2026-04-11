using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject mainMenuPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Retry()
    {
       Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    public void ReturnToMainMenu()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        Time.timeScale = 1f;
    }
}