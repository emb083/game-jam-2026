using UnityEngine;
using UnityEngine.UI;

public class RadialSliderGradient : MonoBehaviour
{
    public Image fillImage;
    public Gradient colorGradient;

    private Slider RadialSlider;

    private void Start()
    {
        RadialSlider = GetComponent<Slider>();
        RadialSlider.value = 0.99f;
    }

    void Update()
    {
        // Update color based on the current fill amount (0 to 1)
        fillImage.color = colorGradient.Evaluate(fillImage.fillAmount);

        if (RadialSlider.value > RadialSlider.minValue)
        {
            RadialSlider.value -= 0.03f * Time.deltaTime;
            RadialSlider.value = Mathf.Clamp(RadialSlider.value, RadialSlider.minValue, RadialSlider.maxValue);
        }
        else
        {
            // Miss Order
        }
    }

    void SetTimer()
    {
        RadialSlider.value = 30;
    }
}
