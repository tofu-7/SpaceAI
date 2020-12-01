using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float arbitraryDist = 5; //radius of the circle 
    float minDepth = -10; //includes objects with a Z coordinate that are greater than or equal to this value
    float maxDepth = 10; //includes objects with a Z coordinate that are less than or equal to this value
    //startPoint = new Vector2();
    ShipScript shipScript;
    shipScript.cs;
    new Vector2 center = shipScript.cum;

    void Update()
    {
        //Renderer rend;
        //rend = GetComponent<Renderer>();
        //Vector2 center = rend.bounds.center; //gets the center of the sprite attached so we can use that for our center point. It will constantly update every frame to a new point, calculating a new colliderCast.

        public static Collider2D[] OverlapCircleAll(cum, arbitraryDist, int layerMask = DefaultRaycastLayers, minDepth, maxDepth); //layermast is for checking on layers, not any need to chance this (for now)
    }

}
