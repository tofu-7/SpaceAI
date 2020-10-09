using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core
{
    //Property definitions
    public int offSpringCount {get; set;}
    public bool[] validPlace { get; set; } = new bool[4]; //Top, Right, Bottom, Left
    public float offspringMutationChance { get; set; }
    public int sensingRange { get; set; }
    public int mass { get; set; }
    public int MaxStorageCap { get; set; }
    public int currentlyStored { get; set; }

    //Instance Constructor
    public Core()
    {
        validPlace = new bool[4]{true, true, true, true}; //Top, Right, Bottom, Left
        offSpringCount = 1;
        offspringMutationChance = 0.15f;
        sensingRange = 5;
        mass = 1;
        MaxStorageCap = 10;
        currentlyStored = 0;
    }
    public class CorePrefab : MonoBehaviour
    {
        [SerializeField]
        public static Transform corePrefab = default; //TODO: Fix this line
        void Start()
        {
            
        }
        void Update()
        {
            
        }
    }
}
