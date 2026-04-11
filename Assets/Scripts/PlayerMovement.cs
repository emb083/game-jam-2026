using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // set in inspector
    public float movementSpeed = 5f;

    // set in script
    private PlayerActions inputActions;

    private void Start() {
        inputActions = new ();
        inputActions.Enable();
        inputActions.Standard.Enable();
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
            // interact
        }

        if (input.Swap.IsPressed()){
            // swap
        }
            
    }
}
