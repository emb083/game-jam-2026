using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject mainMenuScreen;

    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static WinScreen Instance { get; private set; }

    public void Awake()
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

    public void Update()
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
       if (screen = null)
       {
            return;
       }

       screen.SetActive(true);
    }

    public void HideScreen(GameObject screen)
    {
        if (screen = null)
        {
            return;
        }

        print(screen.name);

        screen.SetActive(false);
    }
    

    public void PlayAgain()
    {         
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    public void ReturnToMainMenu()
    {
        winScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}