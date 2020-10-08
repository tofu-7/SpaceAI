using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ThrusterTraits
{
    topValid = 0,
    leftValid = 1,
    BottomValid = 1,
    RightValid = 1,
    mass = 1,
    specificImpulse = 1, //Force exerted (per s) = specificImpulseVar * resource consumed (per s)
}
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
