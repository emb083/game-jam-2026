using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;

    private int score = 0;

    public int Score => score;

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

    private void Update() => UpdateScoreUI();

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
        // Intentionally does nothing.
        Debug.Log($"Missed order: +0, total score: {score}");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
}
