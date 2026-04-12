using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class OrderTimer : MonoBehaviour
{
    // how many seconds player has to fulfill order
    public float orderTimeLimit = 30f;

    // The TMP text that displays the countdown
    public TMP_Text timerText;

    // runtime 
    public float currentTime;
    public bool timerRunning = false;
    public bool timerExpired = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // timer starts by showing full time limit
        currentTime = orderTimeLimit;
        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning)
            return;

        // reduce timer by amount of real time that's passed
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            timerRunning = false;
            timerExpired = true;

            UpdateTimerDisplay();
            OnTimerExpired();
            return;
        }

        // Keep the UI text updated every frame while timer runs
        UpdateTimerDisplay();
    }

    public void StartTimer()
    {
        currentTime = orderTimeLimit;
        timerRunning = true;
        timerExpired = false;

        UpdateTimerDisplay();
    }

    public void StopTimer()
    {
        // Stops timer without reseting remaining time
        timerRunning = false;
    }

    public void ResetTimer()
    {
        // resets timer back to full without automatically starting it
        currentTime = orderTimeLimit;
        timerRunning = false;
        timerExpired = false;

        UpdateTimerDisplay();
    }

    public bool IsRunning()
    {
        return timerRunning;
    }

    public bool HasExpired()
    {
        return timerExpired;
    }

    public float GetRemainingTime()
    {
        return currentTime;
    }

    private void UpdateTimerDisplay()
    {
        // If no TMP text is assigned, does nothing
        if (timerText == null)
            return;

        // rounds to display better countdown values
        int displaySeconds = Mathf.CeilToInt(currentTime);

        // display as seconds
        timerText.text = displaySeconds.ToString();
    }

    private void OnTimerExpired()
    {
        Debug.Log("Order Expired.");
        // can put rest of fail logic here, not sure how many
        // customers you wanted them to be able to miss before 
        // losing
    }
}
