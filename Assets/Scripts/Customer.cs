using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using TMPro;

public class Customer : MonoBehaviour {
    public float customerSpeed = 1f;
    public List<Sprite> realitySpriteOps = null;
    public List<Sprite> imagineSpriteOps = null;
    [HideInInspector] public GameObject prescription;

    private Sprite realitySprite;
    private Sprite imagineSprite;
    private GameBehavior Game;
    private List<GameObject> waitSpots = null;
    private GameObject targetSpot = null;
    private GameObject currentSpot = null;
    private bool prescDisplayed;
    private GameObject prescBubble;
    private TMP_Text prescText;
    private GameObject despawn;
    private Animator customerAnimator;
    private int realityRand;
    private int imagineRand;
    private ScoreManager ScoreManager;


    void Start(){
        // setting initial vars
        waitSpots = Map.Instance.waitSpots;
        targetSpot = waitSpots[(waitSpots.Count - 1)];
        Game = GameBehavior.Instance;
        prescDisplayed = false;
        prescBubble = this.transform.Find("PrescBubble").gameObject;
        despawn = GameObject.FindWithTag("Despawn");
        customerAnimator = this.GetComponent<Animator>();

        // setting randomized sprites
        SpriteRenderer renderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        realityRand = Random.Range(0, realitySpriteOps.Count);
        realitySprite = realitySpriteOps[realityRand];
        customerAnimator.SetInteger("Sprite", realityRand);

        imagineRand = Random.Range(0, imagineSpriteOps.Count);
        imagineSprite = imagineSpriteOps[imagineRand];
        customerAnimator.SetInteger("Sprite2", imagineRand);

        if (Game.currentState == GameBehavior.MindState.REALITY || Game.currentState == GameBehavior.MindState.DEPRESSED){
            renderer.sprite = realitySprite;
        }
        else if (Game.currentState == GameBehavior.MindState.IMAGINATION || Game.currentState == GameBehavior.MindState.INSANE){
            renderer.sprite = imagineSprite;
        }

        // setting randomized prescription
        int prescRand = Random.Range(0, Map.Instance.medications.Count);
        prescription = Map.Instance.medications[prescRand];
        Rigidbody2D Body = this.GetComponent<Rigidbody2D>();
    }

    void Update(){
        // if order complete, go to despawn
        if (targetSpot == despawn) {
            this.transform.Translate(Vector3.left * customerSpeed * Time.deltaTime);
        }

        // if not at target spot, move
        else if (currentSpot != targetSpot){
            this.transform.position = Vector3.MoveTowards(transform.position, targetSpot.transform.position, (customerSpeed * Time.deltaTime));
        }

        // if next spot in line available, move
        LineSpot curSpotData = null;
        if (currentSpot != null){
            curSpotData = currentSpot.GetComponent<LineSpot>();
            foreach (GameObject spot in waitSpots){
                LineSpot spotData = spot.GetComponent<LineSpot>();
                if (!spotData.occupied && spotData.spotNum == curSpotData.spotNum - 1)
                {
                    targetSpot = spot;
                }
                else if (spotData.occupied && spotData.spotNum == curSpotData.spotNum - 1)
                {
                    customerAnimator.SetBool("Walking", false);
                    customerAnimator.SetBool("AtDesk", true);
                }
            }
        }

        if (OrderTimer.Instance.HasExpired()){
            ScoreManager.Instance.AddMissedOrderPenalty();
            if (GameBehavior.Instance.currentState == GameBehavior.MindState.IMAGINATION || GameBehavior.Instance.currentState == GameBehavior.MindState.INSANE){
                SoundManager.Play(SoundType.ORDER_MISSED_ALIEN);
            }
            else if (GameBehavior.Instance.currentState == GameBehavior.MindState.REALITY || GameBehavior.Instance.currentState == GameBehavior.MindState.DEPRESSED){
                SoundManager.Play(SoundType.ORDER_MISSED_HUMAN);
            }
            Leave();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D c){
        
        if (c.CompareTag("Despawn")) {
            Destroy(gameObject);
        }
        else if (c.CompareTag("LineSpot")) {
            currentSpot = c.gameObject;
            if (currentSpot.GetComponent<LineSpot>().spotNum == 1 && !prescDisplayed){
                // start order
                prescDisplayed = true;
                customerAnimator.SetBool("Walking", false);
                customerAnimator.SetBool("AtDesk", true);
                DisplayPresc();
                OrderTimer.Instance.StartTimer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.CompareTag("LineSpot"))
        {
            customerAnimator.SetBool("Walking", true);
            customerAnimator.SetBool("AtDesk", false);
        }
    }

    private void DisplayPresc() {
        Medication med = prescription.GetComponent<Medication>();
        string displayText = "";

        if (Game.currentState == GameBehavior.MindState.IMAGINATION || Game.currentState == GameBehavior.MindState.INSANE) {
            displayText = med.imagineName;

            bool garble = Game.CheckGarble();
            if (garble) {
                displayText = Game.Garble(displayText);
            }
        }
        else if (Game.currentState == GameBehavior.MindState.REALITY || Game.currentState == GameBehavior.MindState.DEPRESSED) {
            displayText = med.realName;
        }

        // display prescription in speech bubble
        prescText = prescBubble.GetComponentInChildren<TMP_Text>();
        prescText.text = displayText;
        prescText.color = Color.black;
        prescBubble.SetActive(true);

        SoundManager.Play(SoundType.CUSTOMER_ORDER);
    }

    private string Garble(string text){
        // convert to char array
        char[] charArray = text.ToCharArray();

        // make all lowercase
        charArray[0] = char.ToLower(charArray[0]);

        // scramble
        for (int i = 0; i < charArray.Length; i++){
            char temp = charArray[i];
            int randomIndex = Random.Range(i, charArray.Length);
            charArray[i] = charArray[randomIndex];
            charArray[randomIndex] = temp;
        }

        // make new first letter uppercase
        charArray[0] = char.ToUpper(charArray[0]);

        // return full string from char array
        return new string(charArray);
    }

    public bool CheckPresc(GameObject held){
        if (held == prescription){
            return true;
        }
        return false;
    }

    public void Leave(){
        OrderTimer.Instance.timerExpired = false;
        OrderTimer.Instance.StopTimer();
        targetSpot = despawn;
        prescBubble.SetActive(false);
    }

    public void SwapSprite(GameBehavior.MindState state){
        // customerAnimator.SetTrigger("ChangeMode");
        // if (state == GameBehavior.MindState.REALITY){
        //     customerAnimator.SetBool("Alien", false);
        // }
        // else if (state == GameBehavior.MindState.IMAGINATION){
        //     customerAnimator.SetBool("Alien", true);
        // }
    }
}
