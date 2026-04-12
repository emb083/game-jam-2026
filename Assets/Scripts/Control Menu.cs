using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlsScreen;
    [SerializeField] private GameObject mainMenuScreen;

    [Header("Buttons")]
    [SerializeField] private Button backButton;

    public void Start()
    {
        if (backButton != null)
        {
            backButton.onClick.AddListener(Back);
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

    public void Back()
    {
        HideScreen(controlsScreen);
        ShowScreen(mainMenuScreen);
    }
}

