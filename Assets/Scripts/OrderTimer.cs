using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class OrderTimer : MonoBehaviour
{
    // how many seconds player has to fulfill order
    public float orderTimeLimit = 30f;

    // runtime 
    public float currentTime;
    public bool timerRunning = false;
    public bool timerExpired = false;

    // UI display
    public Image fillImage;
    public Gradient colorGradient;
    private Slider RadialSlider;
    private Animation TimerAnim;

    public static OrderTimer Instance {get; private set;}

    void Awake() {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // timer starts by showing full time limit
        currentTime = orderTimeLimit;

        RadialSlider = GetComponent<Slider>();
        RadialSlider.value = 0.99f;

        TimerAnim = GetComponent<Animation>();

        UpdateTimerDisplay();
    }

    // Update is called once per frame
    void Update() {
        if (!timerRunning) {return;}

        // reduce timer by amount of real time that's passed
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f) {
            currentTime = 0f;
            timerRunning = false;
            timerExpired = true;

            UpdateTimerDisplay();
            OnTimerExpired();
            return;
        }

        // Keep the UI updated every frame while timer runs
        UpdateTimerDisplay();
    }

    public void StartTimer() {
        currentTime = orderTimeLimit;
        timerRunning = true;
        timerExpired = false;

        UpdateTimerDisplay();
    }

    public void StopTimer() {
        // Stops timer without reseting remaining time
        timerRunning = false;
        TimerAnim.Stop();
    }

    public void ResetTimer() {
        // resets timer back to full without automatically starting it
        currentTime = orderTimeLimit;
        timerRunning = false;
        timerExpired = false;

        UpdateTimerDisplay();
    }

    public bool IsRunning() {
        return timerRunning;
    }

    public bool HasExpired() {
        return timerExpired;
    }

    public float GetRemainingTime() {
        return currentTime;
    }

    private void UpdateTimerDisplay() {
        TimerAnim.Play("StopwatchHHamds");
        // Update color based on the current fill amount (0 to 1)
        fillImage.color = colorGradient.Evaluate(fillImage.fillAmount);

        RadialSlider.value -= 0.03f * Time.deltaTime;
        RadialSlider.value = Mathf.Clamp(RadialSlider.value, RadialSlider.minValue, RadialSlider.maxValue);
    }

    private void OnTimerExpired() {
        GameBehavior.Instance.ordersMissed += 1;
    }
}
