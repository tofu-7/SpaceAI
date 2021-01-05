using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This is a helper file to cleanup others
public class Thruster
{
    //Traits of all thrusters
    public Vector2 relPos { get; set; } //Position relative to core (in increments of unity units = 64px)
    public int facing { get; set; } //FIRES: [Forward, Back, Left, Right] 

    public Thruster()
    {
    }
}
public class Mouth
{
    //Traits of all Mouths
    public Vector2 relPos { get; set; } //Position relative to core (in increments of unity units = 64px)
    public int facing { get; set; } //EATS: [Forward, Back, Left, Right] 
    public float consumeDist { get; set; }
    public Mouth()
    {
    }
}

public class ShipStruct : MonoBehaviour
{
    public Thruster[] thrustArr;
    Mouth[] mouthArr;

    public void ThrusterDec(Vector2 relPos, int face)
    {
        List<Thruster> thrusters = new List<Thruster>();
        //RELATIVE TO CORE NOT PARENT, YOU TURD STAIN
        thrusters.Add(new Thruster() {relPos = relPos, facing = face});

        thrustArr = thrusters.ToArray();
    }
    public void MouthDec(Vector2 relPos, int face, float consDis)
    {
        List<Mouth> mouths = new List<Mouth>();
        mouths.Add(new Mouth(){ relPos = relPos, facing = face, consumeDist = consDis });

        mouthArr = mouths.ToArray();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Thruster[] GetThrusters()
    {
        return thrustArr;
    }
    public Mouth[] GetMouths()
    {
        return mouthArr;
    }
}
