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
    public int[] parentside {get; set;} = new int[4]; //Top, Right, Bottom, Left
    public bool[] validPlace { get; set; } = new bool[4]; //Top, Right, Bottom, Left
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

    // Start is called before the first frame update
    void Start()
    {
        List<Part> parts = new List<Part>();

        parts.Add(new Part(){
            partType=0, 
            partGen=0,
            parentside = new int[4] { 0, 0, 0, 0 },
            validPlace = new bool[4]{true, true, true, true},
            offspringCount=2,
            offspringMutationChance=0.5f,
            sensingRange=25,    
            MaxStorageCap=30,
            currentlyStored=0}); //TODO: Add Thruster, then parentside, to implement offset 
        parts.Add(new Part(){
            partType=2, 
            partGen=1, 
            parentside=new int[4]{1, 0, 0, 0},//Top, Right, Bottom, Left
            validPlace= new bool[4]{false, true, false, true}, //Top, Right, Bottom, Left
            acc = 1});

        Instantiator(parts);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Instantiator(List<Part> _parts){
        GameObject lastObj = null;

        for(int i = 0; i < _parts.Capacity; i++){
            Part curPart = _parts[i];
            GameObject curObj;

            if (curPart.partType == 0){
                curObj = Instantiate(corePrefab);
            }
            else if(curPart.partType == 1){
                curObj = Instantiate(mouthPrefab);
            }
            else  /*(curPart.partType == 2)*/{
                curObj = Instantiate(thrusterPrefab);
            }

            if(curPart.partGen == 0)
            {
                curObj.transform.position = new Vector2(25, 25);
            }
            if (curPart.partGen > 0)
            {   
                curObj.transform.SetParent(lastObj.transform);
                curObj.transform.localPosition = new Vector2(-(curPart.parentside[1] * curPart.partGen) + (curPart.parentside[3] * curPart.partGen),
                                    -(curPart.parentside[0] * curPart.partGen) + (curPart.parentside[2] * curPart.partGen));
            }
            
            Debug.Log(curObj.transform.localPosition);
            lastObj = curObj;
        }
    }
}
