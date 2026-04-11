using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MedicationPopup : MonoBehaviour
{
    [SerializeField] private GameObject medLabel;
    [SerializeField] private TMP_Text labelText;
    [SerializeField] [TextArea] private string labelContent;
    [SerializeField] private string PlayerTag = "Player";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (medLabel != null)
            medLabel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(PlayerTag)) 
            return; 
        
        if (medLabel != null)
            medLabel.SetActive(true);

        if (labelText != null)
            labelText.text = labelContent;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(PlayerTag)) 
            return;
        if (medLabel != null)
            medLabel.SetActive(false);
    }
}
