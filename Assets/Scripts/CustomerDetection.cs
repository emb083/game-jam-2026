using UnityEngine;

public class CustomerDetection : MonoBehaviour
{
    // set in inspector
    public GameObject firstSpot;
    
    public GameObject CustomerAtCounter(){
        if (firstSpot.GetComponent<LineSpot>().occupied){
            return firstSpot.GetComponent<FirstSpot>().GetCurrentCustomer();
        }
        return null;
    }
}
