using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    Camera cam;
    Vector3 mousePos = Vector3.zero; //unfortunately has to be global so we don't reset to 0 everytime we release mouse button
    ShipStruct shipStruct = new ShipStruct();
    float energy;

    void Start()
    {
        cam = Camera.main;
        shipStruct.ThrusterDec(new Vector2(0, -.5f), 1);  //bottom one
        shipStruct.ThrusterDec(new Vector2(-.5f, 0), 2);  //left one
        shipStruct.ThrusterDec(new Vector2(-.5f, -.5f), 0); //top left one
        shipStruct.ThrusterDec(new Vector2(.5f, 0), 3); //right one
        shipStruct.ThrusterDec(new Vector2(.5f, .5f), 0); //top right one

        shipStruct.MouthDec(new Vector2(0, 1), 0, 10f); //top one
        gameObject.GetComponent<Rigidbody2D>().mass = 2.1f;
        energy = 7;
    }

    void Update()
    {
        GameObject coreObj = gameObject;
        Rigidbody2D coreRb = coreObj.transform.GetComponent<Rigidbody2D>();
        bool[] wasd = wasdInput();
        Thruster[] _t = shipStruct.GetThrusters();
        Mouth[] _m = shipStruct.GetMouths();
        const float ff =2f;
        float hrz = Input.GetAxis("Horizontal");
        float vrt = Input.GetAxis("Vertical");
        float rot = 0;
        const float rotSpeed = 60;

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
         
      //  Debug.Log(Input.mousePosition + ", " + mousePos); //Just a debug
       // Debug.DrawLine(transform.position, mousePos, Color.green); //Just a debug

        CamMove();
      //  Debug.Log(wasd[0] + ", " + wasd[1] + ", " + wasd[2] + ", " + wasd[3]);//Use this to fix your code https://www.youtube.com/watch?v=Oh-HCrGgcH8

        coreRb.AddForce(new Vector2(hrz * ff, vrt * ff));

        if (Input.GetKey(KeyCode.Comma))
        {
            rot++;
            Mathf.Clamp(rot, -1, 1);
        }
        if (Input.GetKey(KeyCode.Period))
        {
            rot--;
            Mathf.Clamp(rot, -1, 1);
        }
        rot *= rotSpeed;
        rot *= Time.deltaTime;

        transform.Rotate(0, 0, rot);
        Vector2 velVector = new Vector2(coreRb.velocity.x + transform.position.x, coreRb.velocity.y + transform.position.y);
        Debug.DrawLine(transform.position, new Vector2(4 * coreRb.velocity.x + transform.position.x, 4 * coreRb.velocity.y + transform.position.y), Color.red);
        /**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**/
        int mask = LayerMask.NameToLayer("Resc");
        float mass = gameObject.GetComponent<Rigidbody2D>().mass;


        Debug.Log(mask.ToString());

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), Mathf.Infinity);
        // Does the ray intersect any objects excluding the player layer

        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * hit.distance, Color.white);
            //Debug.Log("Did Hit, " + hit.collider.gameObject.layer.ToString());
            if (hit.distance <= 2.5f)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * hit.distance, Color.yellow);
                if (Input.GetKey(KeyCode.Space) && energy < 10) //that float in there is consumption dist
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * hit.distance, Color.red);
                    Destroy(hit.collider.gameObject);
                    energy += 1f;
                }
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }

        energy -= 0.25f * Time.deltaTime;
        energy = (float)Math.Round(energy, 2);
        if (energy <= 0)
        {
            Debug.Break();
        }
        Debug.Log(energy);
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
