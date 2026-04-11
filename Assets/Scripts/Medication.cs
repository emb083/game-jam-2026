using UnityEngine;

public class Medication : MonoBehaviour
{
    // set in inspector
    public string realName;
    public string imagineName;

    private void OnTriggerEnter2D(Collider2D c) {
        if (c.CompareTag("Player")) {
            DisplayTooltip();
        }
    }

    public void DisplayTooltip(){
        print($"Real: {realName}\nImaginary: {imagineName}");
    }
}
