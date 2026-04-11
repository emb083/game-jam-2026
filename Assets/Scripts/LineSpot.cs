using UnityEngine;

public class LineSpot : MonoBehaviour
{
    public bool occupied;
    public int spotNum;

    void Start() {
        occupied = false;
    }

    private void OnTriggerEnter2D(Collider2D c){
        if (c.gameObject.CompareTag("Customer")) {
            occupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D c){
        if (c.gameObject.CompareTag("Customer")) {
            occupied = false;
        }
    }
}
