using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using TMPro;

public class Customer : MonoBehaviour {
    public float customerSpeed = 1f;
    public List<Sprite> realitySpriteOps = null;
    public List<Sprite> imagineSpriteOps = null;
    public GameObject currentSpot = null;

    private Sprite realitySprite;
    private Sprite imagineSprite;
    private GameBehavior Game;
    private GameObject prescription;
    private List<GameObject> waitSpots = null;
    private GameObject targetSpot = null;
    private bool prescDisplayed;
    private GameObject prescriptionBubble;
    private TMP_Text prescriptionText;


    void Start(){
        // setting initial vars
        waitSpots = Map.Instance.waitSpots;
        targetSpot = waitSpots[(waitSpots.Count - 1)];
        Game = GameBehavior.Instance;
        prescDisplayed = false;
         

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
    }

    void Update(){
        if (currentSpot != targetSpot){
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
        if (c.CompareTag("LineSpot")) {
            currentSpot = c.gameObject;

            if (currentSpot.GetComponent<LineSpot>().spotNum == 1 && !prescDisplayed){
                prescDisplayed = true;
                DisplayPresc();
            }
        }
    }

    private void DisplayPresc() {
        if (Game == null) {
            Debug.LogError("Game is null on " + gameObject.name);
            return;
        }

        if (prescription == null) {
            Debug.LogError("Prescription is null on " + gameObject.name);
            return;
        }

        Medication med = prescription.GetComponent<Medication>();
        if (med == null) {
            Debug.LogError("Prescription has no Medication component on " + gameObject.name);
            return;
        }

        string displayText = "";

        if (Game.currentState == GameBehavior.MindState.IMAGINATION || Game.currentState == GameBehavior.MindState.IMAGINATION_LOCKED) {
            displayText = med.imagineName;

            bool garble = Game.CheckGarble();
            if (garble) {
                displayText = Garble(displayText);
            }
        }

        else if (Game.currentState == GameBehavior.MindState.REALITY || Game.currentState == GameBehavior.MindState.REALITY_LOCKED) {
            displayText = med.realName;
        }

        // display prescription in speech bubble
        if (prescriptionBubble != null) {
            prescriptionBubble.SetActive(true);
        }

        else
        {
            Debug.LogError("prescriptionBubble is null on " + gameObject.name);
            return;
        }

        if (prescriptionText == null) {
            prescriptionText = prescriptionBubble.GetComponentInChildren<TMP_Text>();
        }

        if (prescriptionText == null)
        {
            Debug.LogError("No TMP_Text found inside prescriptionBubble on " + gameObject.name);
            return;
        }

        prescriptionText.text = displayText;

        print($"Prescription: {displayText}");
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
}
