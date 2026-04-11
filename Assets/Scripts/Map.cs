using UnityEngine;
using System;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
    public static Map Instance {get; private set;}

    public List<GameObject> medications;

    void Awake() {
        Instance = this;
    }
}
