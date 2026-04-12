using UnityEngine;
using UnityEngine.UI;

public class MeterManipulation : MonoBehaviour
{

    public Slider BoredomBar;
    public Slider InsanityMeter;
    public float Speed = 0.2f;
    public bool Reality = true;


    // Update is called once per frame
    void Update()
    {
        float change = Speed * Time.deltaTime;

        if (Reality)
        {
            BoredomBar.value += change;
            InsanityMeter.value -= change;
        }
        else
        {
            InsanityMeter.value += change;
            BoredomBar.value -= change;
        }

        BoredomBar.value = Mathf.Clamp(BoredomBar.value, BoredomBar.minValue, BoredomBar.maxValue);
        InsanityMeter.value = Mathf.Clamp(InsanityMeter.value, InsanityMeter.minValue, InsanityMeter.maxValue);
    }

    public void SetReality()
    {
        Reality = true;
    }

    public void SetImagination()
    {
        Reality = false;
    }

}

