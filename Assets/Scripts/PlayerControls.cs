using UnityEngine;

public class PlayerControls : MonoBehaviour {
    // set in inspector
    public float movementSpeed = 5f;

    // set in script
    private PlayerActions inputActions;
    private GameObject interactable;
    private GameObject holding;

    private void Start() {
        inputActions = new ();
        inputActions.Enable();
        inputActions.Standard.Enable();

        interactable = null;
        holding = null;
    }

    private void Update() {
        PlayerActions.StandardActions input = inputActions.Standard;

        // on key release

        if (input.WalkUp.WasReleasedThisFrame()){
            // end animation, go to idle
        }

        if (input.WalkDown.WasReleasedThisFrame()){
            // end animation, go to idle
        }

        if (input.WalkLeft.WasReleasedThisFrame()){
            // end animation, go to idle
        }

        if (input.WalkRight.WasReleasedThisFrame()){
            // end animation, go to idle
        }

        // on key press

        if (input.WalkUp.IsPressed()){
            this.transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (input.WalkDown.IsPressed()){
            this.transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
        }

        if (input.WalkLeft.IsPressed()){
            this.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if (input.WalkRight.IsPressed()){
            this.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }

        if (input.Interact.IsPressed()){
            if (interactable != null){
                if (interactable.tag == "orderSpot"){
                    // interact with customer
                }
                else if (interactable.tag == "Med"){
                    GameObject heldObj = this.transform.Find("Held").gameObject;
                    heldObj.SetActive(true);
                    GameObject grabbedMed = interactable.transform.parent.gameObject;
                    Sprite grabbedSprite = grabbedMed.GetComponent<SpriteRenderer>().sprite;
                    heldObj.GetComponent<SpriteRenderer>().sprite = grabbedSprite;

                    holding = interactable;
                }
            }
        }

        if (input.Swap.IsPressed()){
            // swap
        }

        if (input.Pause.IsPressed()) {
            PauseMenu.Instance.OpenPauseMenu();
        }
            
    }

    private void OnTriggerEnter2D (Collider2D c){
        interactable = c.gameObject;
    }

    private void OnTriggerExit2D (){
        interactable = null;
    }
}
