using UnityEngine;
using UnityEngine.UI;

public class MeterManipulation : MonoBehaviour
{
    public Slider BoredomBar;
    public Slider InsanityMeter;
    public float Speed = 0.03f;
    public GameBehavior.MindState mode;

    void Start(){
        mode = GameBehavior.Instance.currentState;
    }

    // Update is called once per frame
    void Update() {
        mode = GameBehavior.Instance.currentState;
        float change = Speed * Time.deltaTime;

        if (mode == GameBehavior.MindState.REALITY) {
            if (BoredomBar.value < BoredomBar.maxValue){
                BoredomBar.value += change;
                InsanityMeter.value -= change;
            }
            else {
                GameBehavior.Instance.ChangeState(GameBehavior.MindState.DEPRESSED);
            }
        }
        else if (mode == GameBehavior.MindState.IMAGINATION) {
            if (InsanityMeter.value < InsanityMeter.maxValue) {
                InsanityMeter.value += change;
                BoredomBar.value -= change;
            }
            else {
                GameBehavior.Instance.ChangeState(GameBehavior.MindState.INSANE);
            }
        }

        BoredomBar.value = Mathf.Clamp(BoredomBar.value, BoredomBar.minValue, BoredomBar.maxValue);
        InsanityMeter.value = Mathf.Clamp(InsanityMeter.value, InsanityMeter.minValue, InsanityMeter.maxValue);

    }
}

