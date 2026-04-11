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
        RadialSlider.value = 30;
    }

    void Update()
    {
        // Update color based on the current fill amount (0 to 1)
        fillImage.color = colorGradient.Evaluate(fillImage.fillAmount);

        RadialSlider.value -= 1;
    }

    void SetTimer()
    {
        RadialSlider.value = 30;
    }
}
