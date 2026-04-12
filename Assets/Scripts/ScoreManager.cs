using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;

    private int score = 0;
    private int missedOrders = 0;

    public int Score => score;
    public int MissedOrders => missedOrders;

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

        UpdateScoreUI();
    }

    public void AddSuccessfulOrderPoints(float timeLeft)
    {
        int points = 100 + Mathf.Max(0, Mathf.FloorToInt(timeLeft));
        score += points;
        UpdateScoreUI();
        Debug.Log($"Successful order: +{points}, total score: {score}");
    }

    public void AddWrongOrderPenalty()
    {
        score -= 100;
        UpdateScoreUI();
        Debug.Log($"Wrong order: -100, total score: {score}");
    }

    public void AddMissedOrderPenalty()
    {
        missedOrders++;
        Debug.Log($"Missed order: +0, total score: {score}, missed orders: {missedOrders}");

        if (missedOrders >= 3)
        {
            if (LoseScreen.Instance != null)
            {
                LoseScreen.Instance.ShowLoseScreen();
            }
            else
            {
                Debug.LogWarning("LoseScreen.Instance is null.");
            }
        }
    }

    public void ResetScore()
    {
        score = 0;
        missedOrders = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}
