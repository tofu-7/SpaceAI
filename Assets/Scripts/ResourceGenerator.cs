using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceGenerator : MonoBehaviour
{

    public static int ResourceCap = 50; //stream This Guitar by Fraxiom cus they are poggers
    public static int mouthCap = 10; //omnom
    public static int thrusterCap = 10; //woosh

    public bool renewableResources = true; //if only we had this setting irl
    public bool renewableComponents = false; //TODO Implement this
    
    //put prefabs for stuff here
    public Transform resource; 
    public Transform mouth;
    public Transform thruster; //TODO is there a better way of doing this

    void Start() //initial generation of resources runs on sim start
    {
        for (int i= 0; i<ResourceCap; i++) //initially generates resources
        {
            Transform newR = Instantiate(resource).GetComponent<Transform>();
            newR.localPosition = new Vector3(Random.Range(0f, GlobalEnvironmentLib.xBound), Random.Range(0f, GlobalEnvironmentLib.yBound), 0);
        }
        for (int i = 0; i < mouthCap; i++) //initially generates mouths
        {
            Transform newR = Instantiate(mouth).GetComponent<Transform>();
            newR.localPosition = new Vector3(Random.Range(0f, GlobalEnvironmentLib.xBound), Random.Range(0f, GlobalEnvironmentLib.yBound), 0);
        }
        for (int i = 0; i < thrusterCap; i++) //initially generates thrusters
        {
            Transform newR = Instantiate(thruster).GetComponent<Transform>();
            newR.localPosition = new Vector3(Random.Range(0f, GlobalEnvironmentLib.xBound), Random.Range(0f, GlobalEnvironmentLib.yBound), 0);
        }
    }

    void Update() //renew stuff if needed
    {
        if(renewableResources && resourceCount() < ResourceCap) //does thing if less resources than cap
        {
            for (int i = 0; i < ResourceCap-resourceCount(); i++) //makes new resources to cap
            {
                Transform newR = Instantiate(resource).GetComponent<Transform>();
                newR.localPosition = new Vector3(Random.Range(0f, GlobalEnvironmentLib.xBound), Random.Range(0f, GlobalEnvironmentLib.yBound), 0);
            }
        }

        //TODO implement renewable components
    }

    private int resourceCount() //get a count of all resources in the simulation (warning dont look if you value good code)
    {
        int resourcecount = 0; //name is appropriate

        
        GameObject[] arraycount = GameObject.FindGameObjectsWithTag("Resource");



        for(int i = 0; i<ee.Length; i++) //loop over every object
        {
            if (ee[i].name.Contains("Resource")) { resourcecount++; } //oh god y
        }

        return resourcecount; //i think it works O_O
       
    }
}
