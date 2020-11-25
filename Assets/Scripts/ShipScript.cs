using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Thruster
{
    //Traits of all thrusters
    public Vector2 relPos {get; set;} //Position relative to core (in increments of unity units = 64px)
public int facing {get; set; } //FIRES: [Forward, Back, Left, Right] 

    public Thruster()
    {
    }
}
public class Mouth
{
    //Traits of all thrusters
    public Vector2 relPos { get; set; } //Position relative to core (in increments of unity units = 64px)
    public int facing { get; set; } //EATS: [Forward, Back, Left, Right] 

    public Mouth()
    {
    }
}

public class ShipScript : MonoBehaviour
{
    [SerializeField]
    public GameObject coreObj;

    List<Thruster> thrusters = new List<Thruster>();
    List<Mouth> mouths = new List<Mouth>();
    void Start()
    {
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
        { relPos = new Vector2(0, 1), facing = 0}); //top jop topperson one
    }

void Update()
    {
        Rigidbody2D coreRb = coreObj.GetComponent<Rigidbody2D>();
        CompositeCollider2D coreCol = coreObj.GetComponent<CompositeCollider2D>();
        Vector2 cum = coreRb.centerOfMass; //we use cum as our start point

        Thruster[] thrustArr = thrusters.ToArray();
        Mouth[] mouthArr = mouths.ToArray();
        int partCount = thrustArr.Length + mouthArr.Length + 1; 


        coreRb.mass = partCount; //we're assuming all of our parts have a mass of 1
        for(int i = 0; i < thrustArr.Length; i++)
		{
            cum.x += thrustArr[i].relPos.x;
            cum.y += thrustArr[i].relPos.y;
        }
        for (int j = 0; j < mouthArr.Length; j++)
        {
            cum.x += mouthArr[j].relPos.x;
            cum.y += mouthArr[j].relPos.y;
        }

        cum.x /= partCount;
        cum.y /= partCount;
        Debug.Log(cum);
    }
}
