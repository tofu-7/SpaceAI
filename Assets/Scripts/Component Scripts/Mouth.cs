using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
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
public class MouthTraits
{
    //Property definitions
    public bool[] validPlace { get; set; } = new bool[4]; //Top, Right, Bottom, Left
    public int mass { get; set; }
    public int consumeRadius { get; set; }
    public int consumeMaxVel { get; set; }
    public int consumeRate { get; set; }

    //Instance Constructor
    public MouthTraits()
    {
        validPlace = new bool[4] { false, true, true, true }; //Top, Right, Bottom, Left
        mass = 1;
        consumeRadius = 1;
        consumeMaxVel = 2;
        consumeRate = 1;
    }
}