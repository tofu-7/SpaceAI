using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
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

public class ThrusterTraits
{
    //Property definitions
    public bool[] validPlace { get; set; } = new bool[4]; //Top, Right, Bottom, Left
    public int mass { get; set; }
    public float specificImpulse { get; set; }

    //Instance Constructor
    public ThrusterTraits()
    {
        validPlace = new bool[4] { true, true, false, true }; //Top, Right, Bottom, Left
        mass = 1;
        specificImpulse = 1f; //Force exerted (per s) = specificImpulseVar * thrusters' resource consumed (per s)
    }
}