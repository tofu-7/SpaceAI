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
    public int parentside {get; set;} //Top, Right, Bottom, Left == 0, 1, 2, 3
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
            validPlace= new bool[4]{true, true, true, true},
            offspringCount=2,
            offspringMutationChance=0.5f,
            sensingRange=25,    
            MaxStorageCap=30,
            currentlyStored=0}); //TODO: Add Thruster, then parentside, to implement offset 

        Instantiator(parts);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Instantiator(List<Part> _parts){
        int partsArr;
        for(int i = 0; i < _parts.Capacity; i++){
            Part partVal = _parts[i];
            if(partVal.partType == 0){
                Instantiate(corePrefab);
            }
            else if(partVal.partType == 1){
                Instantiate(mouthPrefab);
            }
            else if(partVal.partType == 2){
                Instantiate(thrusterPrefab);
            }
        }
    }
}
