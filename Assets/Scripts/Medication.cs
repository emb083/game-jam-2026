using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Medication : MonoBehaviour
{
    // set in inspector
    public string realName;
    public string imagineName;

    // set in script
    private GameObject tooltip;
    private GameBehavior Game;

    void Start(){
        tooltip = transform.parent.transform.Find("Tooltip").gameObject;
        Game = GameBehavior.Instance;
    }

    private void OnTriggerEnter2D(){
        string displayText = "";

        if (Game.currentState == GameBehavior.MindState.IMAGINATION) {
            displayText = imagineName;
        }
        else if (Game.currentState == GameBehavior.MindState.REALITY || Game.currentState == GameBehavior.MindState.DEPRESSED) {
            displayText = realName;
        }
        else if (Game.currentState == GameBehavior.MindState.INSANE){
            displayText = GameBehavior.Instance.Garble(imagineName);
        }

        TMP_Text tooltipText = tooltip.GetComponentInChildren<TMP_Text>();
        tooltipText.text = displayText;
        tooltip.SetActive(true);
    }

    private void OnTriggerExit2D(){
        tooltip.SetActive(false);
    }
}
