using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    public GameObject core { get; set; }
    public GameObject mouth { get; set; }
    public GameObject thruster { get; set; }

    public CoreTraits coreTraits { get; set; }
    public ThrusterTraits thrusterTraits { get; set; }
    public MouthTraits mouthTraits { get; set; }

    public Ship()
    {
        coreTraits = new CoreTraits();
        thrusterTraits = new ThrusterTraits();
        mouthTraits = new MouthTraits();
    }
}
