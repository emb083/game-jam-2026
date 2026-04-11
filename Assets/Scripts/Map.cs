using UnityEngine;
using System;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    public static Map Instance {get; private set;}

    public List<GameObject> medications;
    public List<GameObject> waitSpots;

    void Awake() {
        Instance = this;
    }
}
