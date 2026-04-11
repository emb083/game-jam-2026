using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour {
    public float customerSpeed = 0.4f;
    public List<Sprite> realitySpriteOps;
    public List<Sprite> imagineSpriteOps;

    private Sprite realitySprite;
    private Sprite imagineSprite;
}

private void Start(){
    // setting randomized sprites
    int realityRand = Random.Range(0, realitySpriteOps.Count());
    realitySprite = realitySpriteOps[realityRand];

    int imagineRand = Random.Range(0, imagineSpriteOps.Count());
    imagineSprite = imagineSpriteOps[imagineRand];
}
