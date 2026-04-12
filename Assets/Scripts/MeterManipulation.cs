using UnityEngine;
using UnityEngine.UI;

public class MeterManipulation : MonoBehaviour
{

    public Slider BoredomBar;
    public Slider InsanityMeter;
    public float Speed = 0.03f;
    public bool Reality = true;


    // Update is called once per frame
    void Update()
    {
        float change = Speed * Time.deltaTime;

        if (Reality)
        {
            if (BoredomBar.value < BoredomBar.maxValue)
            {
                BoredomBar.value += change;
                InsanityMeter.value -= change;
                print(BoredomBar.value.ToString());
            }
            else
            {
                // Call Depressive Mode
            }
        }
        else
        {
            if (InsanityMeter.value < InsanityMeter.maxValue)
            {
                InsanityMeter.value += change;
                BoredomBar.value -= change;
            }
            else
            {
                // Call Insanity Mode
            }
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

