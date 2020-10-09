using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ThrusterTraits
{
}
public static class Thruster
{
    static bool[] validPlace = new bool[4]{true, true, false, true}; //Top, Right, Bottom, Left
    static int mass = 1;
    static float specificImpulse = 1f; //Force exerted (per s) = specificImpulseVar * thrusters' resource consumed (per s)
}
