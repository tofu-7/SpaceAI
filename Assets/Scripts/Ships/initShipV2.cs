using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Part
{             
    //Traits of all parts
     /*
        Core = 0
        Mouth = 1
        Thruster = 2
        */
    public int partType {get; set;}
    public int partGen {get; set;} //Generation (steps away from core)
    public int parentSide {get; set;} //Top, Right, Bottom, Left
    public bool[] validPlace { get; set; } = new bool[4]; //Top, Right, Bottom, Left
    public int parentInd { get; set; }
    public GameObject partObj { get; set; }
    //Core-specific
    public int offspringCount { get; set; }
    public float offspringMutationChance { get; set; }
    public float sensingRange { get; set; }
    public int MaxStorageCap { get; set; }
    public int currentlyStored { get; set; }
    //Thruster-specific
    public float acc { get; set; }
    //Mouth-specific
    public int consumeRadius { get; set; }
    public int consumeMaxVel { get; set; }
    public int consumeRate { get; set; }
    //Constructor
    public Part()
    {
    }
}
public class initShipV2 : MonoBehaviour
{
    [SerializeField]
    GameObject corePrefab;
    [SerializeField]
    GameObject mouthPrefab;
    [SerializeField]
    GameObject thrusterPrefab;

    [SerializeField]
    public bool isFollowed;
    [SerializeField]
    public Camera _cam;

    List<Part> parts = new List<Part>();

    // Start is called before the first frame update
    void Start()
    {
        /* Index diagram 
         *    |5|...|6|
         *    |3||2||4|
         *    ...|0|...
         *    ...|1|...
         **/
        //Add new starting parts here
        parts.Add(new Part()
        { //ind == 0
            partType=0, //check initializer in class for info
            partGen=0, //steps away from core
            validPlace = new bool[4]{true, true, true, true}, //checks to see which sides are valid (Top, Right, Bottom, Left)  (This is before rotation, so relative to part)
            //part-specific traits
            offspringCount =2, 
            offspringMutationChance=0.5f,
            sensingRange=25,    
            MaxStorageCap=30,
            currentlyStored=0
        });

        parts.Add(new Part()
        { //ind == 1
            partType=2, 
            partGen=1,
            parentInd = 0, //What index of this parts list the parent is in
            parentSide = 0, //which side the parent part is on (order is Top, Right, Bottom, Left)
            validPlace = new bool[4]{false, true, true, true}, //Top, Right, Bottom, Left (This is before rotation, so relative to part)
            //part-specific traits
            acc = 1
        });

        parts.Add(new Part()
        { //ind == 2
            partType = 1,
            partGen = 1,
            parentInd = 0, //What index of this parts list the parent is in
            parentSide = 2,//Top, Right, Bottom, Left
            validPlace = new bool[4] { false, true, true, true }, //Top, Right, Bottom, Left  (This is before rotation, so relative to part)
            //part-specific traits
            consumeRadius = 5,
            consumeMaxVel = 2,
            consumeRate = 3 
        });

        parts.Add(new Part() //ind == 3
        {
            partType = 2,
            partGen = 2,
            parentInd = 2, //What index of this parts list the parent is in
            parentSide = 1,//Top, Right, Bottom, Left
            validPlace = new bool[4] { false, true, true, true }, //Top, Right, Bottom, Left (This is before rotation, so relative to part)
            //part-specific traits
            acc = 1
        });

        parts.Add(new Part() //ind == 4
        {
            partType = 2,
            partGen = 2,
            parentInd = 2, //What index of this parts list the parent is in
            parentSide = 3,//Top, Right, Bottom, Left
            validPlace = new bool[4] { false, true, true, true }, //Top, Right, Bottom, Left (This is before rotation, so relative to part)
            //part-specific traits
            acc = 1
        });

        parts.Add(new Part() //ind == 5
        {
            partType = 2,
            partGen = 3,
            parentInd = 3, //What index of this parts list the parent is in
            parentSide = 3,//Top, Right, Bottom, Left
            validPlace = new bool[4] { false, true, true, true }, //Top, Right, Bottom, Left (This is before rotation, so relative to part)
            //part-specific traits
            acc = 1
        });

        parts.Add(new Part() //ind == 6
        {
            partType = 2,
            partGen = 3,
            parentInd = 4, //What index of this parts list the parent is in
            parentSide = 1,// side of the parent that this child connects to: Top, Right, Bottom, Left 
            validPlace = new bool[4] { false, true, true, true }, //Top, Right, Bottom, Left (This is before rotation, so relative to part)
            //part-specific traits
            acc = 1
        });

        Instantiator(parts); //Creates our starting ship from provided list
        CamSetup(parts);
    }

    // Update is called once per frame
    void Update()
    {
        Thrusters(parts);
        Mouths(parts);
    }

    void Mouths(List<Part> _mouths)
    {

    }

    void Thrusters(List<Part> _parts)
    {
        Part[] _partsArr = _parts.ToArray(); //Array to feed for loop length
        Rigidbody2D coreRB = _parts[0].partObj.GetComponent<Rigidbody2D>();
        BoxCollider2D shipCollider = coreRB.GetComponent<BoxCollider2D>();
        /*TODO:
         * =Get rid of all rigidbody but core
         * =Expand core collider as ship expands
         * =Adjust mass accordingly as ships mass grows
         * -Apply proper forces to core's rigid body as if it were the thrusters
        **/
        for (int i = 0; i < _partsArr.Length; i++)
        {
            if (_parts[i].partType == 2) {
                if (Input.GetKey(KeyCode.D) && _parts[i].parentSide == 3)
                {
                   coreRB.AddForceAtPosition(new Vector2(10, 0), 
                       _parts[i].partObj.transform.position);
                    Debug.DrawRay(_parts[i].partObj.transform.position, new Vector2(5 * _parts[i].acc, 0));
                }
                else if (Input.GetKey(KeyCode.A) && _parts[i].parentSide == 1)
                {
                    coreRB.AddForceAtPosition(new Vector2(-10, 0),
                        _parts[i].partObj.transform.position);
                    Debug.DrawRay(_parts[i].partObj.transform.position, new Vector2(-5 * _parts[i].acc, 0));
                }
                else if (Input.GetKey(KeyCode.W) && _parts[i].parentSide == 0)
                {
                    coreRB.AddForceAtPosition(new Vector2(0, 10),
                        _parts[i].partObj.transform.position);
                    Debug.DrawRay(_parts[i].partObj.transform.position, new Vector2(0, 5*_parts[i].acc));
                }
                else if (Input.GetKey(KeyCode.S) && _parts[i].parentSide == 2)
                {
                    coreRB.AddForceAtPosition(new Vector2(0, -10),
                        _parts[i].partObj.transform.position);
                    Debug.DrawRay(_parts[i].partObj.transform.position, new Vector2(0, -5 * _parts[i].acc));
                }
            }
        }
       // Debug.Log(_parts[0].partObj.GetComponent<Rigidbody2D>().velocity);
    }

    void Instantiator(List<Part> _parts){
        Part[] _partsArr = _parts.ToArray();

        for (int i = 0; i < _partsArr.Length; i++){ //Go through every part in the ship

            Part curPart = _parts[i];
            Part lastPart = _parts[curPart.parentInd];
            GameObject curObj;
            GameObject lastObj = lastPart.partObj;
            Rigidbody2D coreRB;
            CompositeCollider2D shipCollider;

            if (curPart.partType == 0) //if: type = number, then: instantiate with that type, and set that parts gameobject to that new one we just instantiated
            { 
                curPart.partObj = Instantiate(corePrefab);
                Debug.Log("Instantiated core");
                coreRB = _parts[i].partObj.GetComponent<Rigidbody2D>();
                shipCollider = coreRB.GetComponent<CompositeCollider2D>();
            }
            else if (curPart.partType == 1)
                curPart.partObj = Instantiate(mouthPrefab);
            else  /*(curPart.partType == 2)*/ //last one in list has to be else, needs to be changed if new part types are added
                curPart.partObj = Instantiate(thrusterPrefab);

            curObj = curPart.partObj; //shortcut variable

            coreRB = _parts[0].partObj.GetComponent<Rigidbody2D>();
            shipCollider = coreRB.GetComponent<CompositeCollider2D>();
            coreRB.mass = 3*_partsArr.Length; //make mass proportional to size (# of parts), add multiplier to maek it feels right

            if (curPart.partGen == 0)
                curObj.transform.position = new Vector2(25, 25); //sets starting point for ship

            if (curPart.partGen > 0) //if: this isn't the core
            {   
                curObj.transform.SetParent(lastObj.transform); //Sets the parent of this object using the parentInd variable
                //parent on side = Top, Right, Bottom, Left
                if (curPart.parentSide == 0) //part placed on bottom
                {
                    curObj.transform.localPosition = Vector2.down; //Creates the offset for this new part, based off of it's parent
                    curObj.transform.localRotation = Quaternion.Euler(0, 0, -180);
                    curPart.validPlace[0] = false;
                 //   shipCollider.size = new Vector2(shipCollider.size.x + 1, shipCollider.size.y);
                  //  shipCollider.offset = new Vector2(shipCollider.offset.x, shipCollider.offset.y - 0.5f);
                }
                else if(curPart.parentSide == 1) //part placed on left
                {
                    curObj.transform.localPosition = Vector2.left;
                    curObj.transform.localRotation = Quaternion.Euler(0, 0, 90); //neg = right, pos = left
                    curPart.validPlace[1] = false;

                //    shipCollider.size = new Vector2(shipCollider.size.x, shipCollider.size.y + 1);
                 //   shipCollider.offset = new Vector2(shipCollider.offset.x - 0.5f, shipCollider.offset.y);
                }
                else if(curPart.parentSide == 2) //part placed on top
                {
                    curObj.transform.localPosition = Vector2.up;
                    curObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    curPart.validPlace[2] = false;

                  //  shipCollider.size = new Vector2(shipCollider.size.x + 1, shipCollider.size.y );
                  //  shipCollider.offset = new Vector2(shipCollider.offset.x, shipCollider.offset.y + 0.5f);
                }
                else if(curPart.parentSide == 3) //part placed on right
                {
                    curObj.transform.localPosition = Vector2.right;
                    curObj.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    curPart.validPlace[3] = false;

               //     shipCollider.size = new Vector2(shipCollider.size.x, shipCollider.size.y + 1);
                //    shipCollider.offset = new Vector2(shipCollider.offset.x + 0.5f, shipCollider.offset.y);
                }
            } 
           
        }
    }
    void CamSetup(List<Part> _parts)
    {

        GameObject shipFollowed = _parts[0].partObj;
        if (isFollowed == true)
        {
            _cam.transform.SetParent(shipFollowed.transform);
            _cam.transform.localPosition = new Vector3(0, 0, -10); //Z axis is actually counter-intuitively reverse (+ is further from cam, - is closer)
        }
        else if (isFollowed == false)
            return;
    }
}
