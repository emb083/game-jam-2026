using UnityEngine;

public class FirstSpot : MonoBehaviour
{
    private GameObject currentCustomer;

    void Start(){
        currentCustomer = null;
    }

    private void OnTriggerEnter2D(Collider2D c){
        currentCustomer = c.gameObject;
    }

    private void OnTriggerEnter2D(){
        currentCustomer = null;
    }

    public GameObject GetCurrentCustomer(){
        return currentCustomer;
    }
}
