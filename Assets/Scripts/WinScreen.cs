using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject mainMenuScreen;

    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Final Score UI")]
    [SerializeField] private TMP_Text finalScoreText;

    public static WinScreen Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
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

    public void ShowWinScreen()
    {
        if (finalScoreText != null && ScoreManager.Instance != null)
        {
            finalScoreText.text = $"Final Score: {ScoreManager.Instance.Score}";
        }

        ShowScreen(winScreen);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScore();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        HideScreen(winScreen);
        ShowScreen(mainMenuScreen);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

