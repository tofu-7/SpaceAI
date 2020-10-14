using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class CoreTraits
{
    //Property definitions
    public int offSpringCount { get; set; }
    public bool[] validPlace { get; set; } = new bool[4]; //Top, Right, Bottom, Left
    public float offspringMutationChance { get; set; }
    public int sensingRange { get; set; }
    public int mass { get; set; }
    public int MaxStorageCap { get; set; }
    public int currentlyStored { get; set; }

    //Instance Constructor
    public CoreTraits()
    {
        validPlace = new bool[4] { true, true, true, true }; //Top, Right, Bottom, Left
        offSpringCount = 1;
        offspringMutationChance = 0.15f;
        sensingRange = 5;
        mass = 1;
        MaxStorageCap = 10;
        currentlyStored = 0;
    }
}