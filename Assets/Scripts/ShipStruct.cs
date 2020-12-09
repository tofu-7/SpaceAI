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

    public Mouth()
    {
    }
}
public class ShipStruct : MonoBehaviour
{

    List<Mouth> mouths = new List<Mouth>();

    public Thruster[] thrustArr;
    Mouth[] mouthArr;

    void ThrusterDec()
    {
        List<Thruster> thrusters = new List<Thruster>();
        //RELATIVE TO CORE NOT PARENT, YOU TURD STAIN
        thrusters.Add(new Thruster()
        { relPos = new Vector2(0, -1), facing = 1 }); //bottom one

        thrusters.Add(new Thruster()
        { relPos = new Vector2(-1, 0), facing = 2 }); //left one
        thrusters.Add(new Thruster()
        { relPos = new Vector2(-1, 1), facing = 0 }); //top left one

        thrusters.Add(new Thruster()
        { relPos = new Vector2(1, 0), facing = 3 }); //right one
        thrusters.Add(new Thruster()
        { relPos = new Vector2(1, 1), facing = 0 }); //top right one
        /**//**/
        mouths.Add(new Mouth()
        { relPos = new Vector2(0, 1), facing = 0 }); //top jop topperson one

        thrustArr = thrusters.ToArray();
    }


    // Start is called before the first frame update
    void Start()
    {
        ThrusterDec();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Thruster GetThruster(int index)
    {
        return thrustArr[index];
    }
}
