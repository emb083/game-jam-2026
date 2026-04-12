using UnityEngine;

public class PlayerControls : MonoBehaviour {
    // set in inspector
    public float movementSpeed = 5f;
    public GameObject pauseMenu;

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
                    GameObject currentCustomer = interactable.GetComponent<CustomerDetection>().CustomerAtCounter();
                    if (currentCustomer != null){
                        CustomerInteract(currentCustomer);
                    }  
                }
                else if (interactable.tag == "Med"){
                    GrabMed();
                }
            }
        }

        if (input.Swap.IsPressed()){
            // swap
        }

        if (input.Pause.IsPressed()) {
            //PauseMenu.Instance.OpenPauseMenu();
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void OnTriggerEnter2D (Collider2D c){
        interactable = c.gameObject;
    }

    private void OnTriggerExit2D (){
        interactable = null;
    }

    private void GrabMed(){
        GameObject heldObj = this.transform.Find("Held").gameObject;
        heldObj.SetActive(true);
        GameObject grabbedMed = interactable.transform.parent.gameObject;
        Sprite grabbedSprite = grabbedMed.GetComponent<SpriteRenderer>().sprite;
        heldObj.GetComponent<SpriteRenderer>().sprite = grabbedSprite;

        holding = interactable;

        SoundManager.Play(SoundType.GRAB);
    }

    private void ClearHold(){
        holding = null;

        GameObject heldObj = this.transform.Find("Held").gameObject;
        heldObj.SetActive(false);
    }

    private void CustomerInteract(GameObject customer){
        GameBehavior Game = GameBehavior.Instance;
        Customer customerData = customer.GetComponent<Customer>();

        if (holding == null){
            if (Game.currentState == GameBehavior.MindState.IMAGINATION || Game.currentState == GameBehavior.MindState.IMAGINATION_LOCKED){
                SoundManager.Play(SoundType.NO_ITEM_ALIEN);
            }
            else if (Game.currentState == GameBehavior.MindState.REALITY || Game.currentState == GameBehavior.MindState.REALITY_LOCKED){
                SoundManager.Play(SoundType.NO_ITEM_HUMAN);
            }
        }
        else {
            if (customerData.prescription == holding) {
                SoundManager.Play(SoundType.ORDER_RIGHT);
                // increment score
            } else {
                SoundManager.Play(SoundType.ORDER_WRONG);
            }
            customerData.Leave();
            ClearHold();
        }
    }
}
