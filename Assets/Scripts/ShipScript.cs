using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipScript : MonoBehaviour
{
    Camera cam;
    Vector3 mousePos = Vector3.zero; //unfortunately has to be global so we don't reset to 0 everytime we release mouse button

    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        GameObject CoreObj = gameObject;
        if (Input.GetMouseButtonUp(1)) //On right click release do:
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Calculate a ray from where we (the person looking at the monitor) see to where the cursor is on click

            mousePos = ray.direction; //transfers that ray into mousepos
            mousePos.z = 0; //this is to make the spat out vector on the same plane as our ship
            mousePos.x *= 10; //this is to convert to in-game units
            mousePos.y *= 10;

            mousePos.x += transform.position.x; //this is to go from relative to core -> world coords
            mousePos.y += transform.position.y;
        }
         
        Debug.Log(Input.mousePosition + ", " + mousePos); //Just a debug
        Debug.DrawLine(transform.position, mousePos, Color.green); //Just a debug
        CamMove();
    }

    bool[] wasdInput() //Realized the old WasdInputDebug.cs was unneccessary and could be kerplunked into a function (Don't use it here, just an old code hoarder)
    {
        bool wtrig = Input.GetKey("w");
        bool atrig = Input.GetKey("a");
        bool strig = Input.GetKey("s");
        bool dtrig = Input.GetKey("d");
        bool[] trigList = { wtrig, atrig, strig, dtrig };

        return trigList;
    }
    void CamMove() //Thanks to this function the camera no longer has to be the child of the ship (helpful somehow?)
    {
        cam.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z - 10);
    }
}
