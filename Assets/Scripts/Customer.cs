using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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

    void Start(){
        waitSpots = Map.Instance.waitSpots;
        targetSpot = waitSpots[(waitSpots.Count - 1)];
        Game = GameBehavior.Instance;
        SpriteRenderer renderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();

        // setting randomized sprites
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
                    if (spotData.occupied is false && spotData.spotNum == curSpotData.spotNum - 1){
                    targetSpot = spot;
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D c){
        if (c.CompareTag("LineSpot")) {
            currentSpot = c.gameObject;
        }
    }
}
