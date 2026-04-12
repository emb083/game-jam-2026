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


    void Start(){
        // setting initial vars
        waitSpots = Map.Instance.waitSpots;
        targetSpot = waitSpots[(waitSpots.Count - 1)];
        Game = GameBehavior.Instance;
        prescDisplayed = false;
        prescBubble = this.transform.Find("PrescBubble").gameObject;
        despawn = GameObject.FindWithTag("Despawn");

        // setting randomized sprites
        SpriteRenderer renderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        int realityRand = Random.Range(0, realitySpriteOps.Count);
        realitySprite = realitySpriteOps[realityRand];

        int imagineRand = Random.Range(0, imagineSpriteOps.Count);
        imagineSprite = imagineSpriteOps[imagineRand];

        if (Game.currentState == GameBehavior.MindState.REALITY || Game.currentState == GameBehavior.MindState.REALITY_LOCKED){
            renderer.sprite = realitySprite;
        }
        else if (Game.currentState == GameBehavior.MindState.IMAGINATION || Game.currentState == GameBehavior.MindState.IMAGINATION_LOCKED){
            renderer.sprite = imagineSprite;
        }

        // setting randomized prescription
        int prescRand = Random.Range(0, Map.Instance.medications.Count);
        prescription = Map.Instance.medications[prescRand];
        Rigidbody2D Body = this.GetComponent<Rigidbody2D>();
    }

    void Update(){
        if (targetSpot == despawn) {
            print("target");
            this.transform.Translate(Vector3.left * customerSpeed * Time.deltaTime);
        }
        else if (currentSpot != targetSpot){
            print("else");
            this.transform.position = Vector3.MoveTowards(transform.position, targetSpot.transform.position, (customerSpeed * Time.deltaTime));
        }

        LineSpot curSpotData = null;
        if (currentSpot != null){
            curSpotData = currentSpot.GetComponent<LineSpot>();
            foreach (GameObject spot in waitSpots){
                LineSpot spotData = spot.GetComponent<LineSpot>();
                    if (!spotData.occupied && spotData.spotNum == curSpotData.spotNum - 1){
                    targetSpot = spot;
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D c){
        
        if (c.CompareTag("Despawn")) {
            print("Destroy");
            Destroy(gameObject);
        }
        else if (c.CompareTag("LineSpot")) {
            currentSpot = c.gameObject;
            if (currentSpot.GetComponent<LineSpot>().spotNum == 1 && !prescDisplayed){
                prescDisplayed = true;
                DisplayPresc();
            }
        }
    }

    private void DisplayPresc() {
        Medication med = prescription.GetComponent<Medication>();
        string displayText = "";

        if (Game.currentState == GameBehavior.MindState.IMAGINATION || Game.currentState == GameBehavior.MindState.IMAGINATION_LOCKED) {
            displayText = med.imagineName;

            bool garble = Game.CheckGarble();
            if (garble) {
                displayText = Game.Garble(displayText);
            }
        }
        else if (Game.currentState == GameBehavior.MindState.REALITY || Game.currentState == GameBehavior.MindState.REALITY_LOCKED) {
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
        targetSpot = despawn;
        prescBubble.SetActive(false);
    }
}
