using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup controlsScreen;
    [SerializeField] private CanvasGroup mainMenuScreen;
    [SerializeField] private CanvasGroup pauseMenuScreen;

    [Header("Buttons")]
    [SerializeField] private Button backButton;

    public void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(Back);
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

    public void Back()
    {
        HideScreen(controlsScreen);
        ShowScreen(mainMenuScreen);
    }
}

