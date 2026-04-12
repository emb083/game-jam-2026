using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    // How long the full workday will last in seconds
    public float shiftLengthSeconds = 480f;

    // Starting hour of the workday in 24 hour format
    public int startHour = 9;

    // Ending hour of the workday in 24 hour format
    public int endHour = 17;

    // Runtime
    public float elapsedTimeSeconds = 0f;
    public bool timerRunning = true;
    public bool shiftEnded = false;

    private int totalGameHours;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        totalGameHours = endHour - startHour;
    }

    private void Update() {
        // if timer is paused or shift ended, do nothing
        if (!timerRunning || shiftEnded) {return;}

        elapsedTimeSeconds += Time.deltaTime;

        // check if end of shift has been reached
        if (elapsedTimeSeconds >= shiftLengthSeconds) {
            elapsedTimeSeconds = shiftLengthSeconds;
            shiftEnded = true;
            timerRunning = false;

            OnShiftEnded();
        }
    }

    private void OnShiftEnded() {
        // Can put end of day logic here but not sure what yall wanted to do with it exactly
        Debug.Log("Shift Ended");
        
        // Show winScreen
    }

    // how many real seconds left in shift
    public float GetRemainingTimeSeconds() {
        return shiftLengthSeconds - elapsedTimeSeconds;
    }

    // shows progress through the shift as value from 0 to 1
    public float GetShiftProgress01() {
        if (shiftLengthSeconds <= 0f) {return 1f;}

        return Mathf.Clamp01(elapsedTimeSeconds / shiftLengthSeconds);
    }

    // shows the current in-game hour in 24 hour format
    public int GetCurrentHour24() {
        float progress = GetShiftProgress01();
        // converts progress into time between startHour and endHour
        float currentGameHourFloat = startHour + (progress * totalGameHours);

        // convert to whole number
        int hour = Mathf.FloorToInt(currentGameHourFloat);

        // Prevent going beyond the end hour during the shift 
        return Mathf.Clamp(hour, startHour, endHour);
    }

    // shows current in-game minute
    public int GetCurrentMinute() {
        float progress = GetShiftProgress01();
        float currentGameHourFloat = startHour + (progress * totalGameHours);

        // get only the decimal portion of the hour calculation for minutes
        float fractionalHour = currentGameHourFloat - Mathf.Floor(currentGameHourFloat);
        int minute = Mathf.FloorToInt(fractionalHour * 60f);

        return Mathf.Clamp(minute, 0, 59);
    }

    // shows formatted time 
    public string GetFormattedTime12Hour() {
        int hour24 = GetCurrentHour24();
        int minute = GetCurrentMinute();

        string amPm = hour24 >= 12 ? "PM" : "AM";
        int hour12 = hour24 % 12;

        if (hour12 == 0) {hour12 = 12;}

        return $"{hour12:00}:{minute:00} {amPm}";
    }

    // pause timer logic
    public void PauseTimer() {timerRunning = false;}

    // unpaused timer logic
    public void ResumeTimer() {
        if (!shiftEnded)
            timerRunning = true;
    }

    // resets timer back to start of shift 
    public void ResetTimer() {
        elapsedTimeSeconds = 0f;
        shiftEnded = false;
        timerRunning = true;
    }
}
