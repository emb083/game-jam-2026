using UnityEngine;
using UnityEngine.UI;

public class MeterManipulation : MonoBehaviour
{

    public Slider BoredomBar;
    public Slider InsanityMeter;
    public float Speed;
    public bool Reality;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Reality)
        {
            BoredomBar.value += Speed;
            InsanityMeter.value -= Speed;
        }
        else
        {
            InsanityMeter.value += Speed;
            BoredomBar.value -= Speed;
        }
    }

    void SetReality()
    {
        Reality = true;
    }

}
