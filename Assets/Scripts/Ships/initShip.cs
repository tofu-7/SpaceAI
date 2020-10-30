using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using UnityEditor;
using System;

//Made by Scott
/* OOOH boi, this i'm for the time being, essentially treating as the 'main' script for the ship,
* only until we manage reproduction/various ship instances
* hence, the name, initShip.cs
*
* I did a fucky wucky.
* 99% chance here I bit off more than I can chew,
* and will spend the coming days trying to break this up into seperate files and classes
*
* In addition to giving u the dumb stuf in Trello and breaking it up into more bite-sized tasks
* 
* TODO:
* -Essentially copy this structure dingle-ass: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort?view=netcore-3.1
**/
public class initShip : MonoBehaviour
{
    //[SerializeField] just means make this variable accesible from Unity GUI so I can click and drag our prefabs in
    [SerializeField]
    GameObject corePrefab;
    [SerializeField]
    GameObject mouthPrefab;
    [SerializeField]
    GameObject thrusterPrefab;

    /* We then create new GameObject vars to use locally,
     * because Unity doesn't like us directly using the Prefabs in here :(
     * **/
    GameObject core;
    GameObject mouth;
    GameObject thruster;

    /* We then create our local trait classes based off the Prefabs trait classes
     * essentially importing them.
     *
     * In addition to introducing our own vars for this script
     * (felt good, might move to a seperate file/class later idk)
     **/
    CoreTraits coreTraits = new CoreTraits();
    ThrusterTraits thrusterTraits = new ThrusterTraits();
    MouthTraits mouthTraits = new MouthTraits();

    int sumMass = 0;
    int mouthCount =1;
    int thrusterCount =1;
    int[] shipBounds = new int[4]{0,0,0,0};  //Top, Right, Bottom, Left (Includes core itself)

    Collider2D[] senseArr;
    /*Determines whether the main camera follows ship or not
     * Might produce bug or not with multiple ships idk :)
     **/
    bool followed = true;

    // Start is called before the first frame update
    void Start()
    {

        SpawnShip();

        //CAM STUFF
        if (followed == true)
        {
            Camera.main.transform.SetParent(core.transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, -10); //Z axis is actually counter-intuitively reverse (+ is further from cam, - is closer)
        }
        else if (followed == false)
        {
            return;
        }
        //END CAM STUFF

    }

    //Ight g, everything here is a WIP and fucked up, and will prob get deleted and replaced in the coming days/hours
    // Update is called once per frame
    void Update()
    {
        //MVMNT STUFF V

        //AAAHHHHHHHHH
        sumMass = coreTraits.mass + thrusterTraits.mass + mouthTraits.mass;
        /*
         //MVMNT STUFF V
       
            
        //AAAHHHHHHHHH
        sumMass = coreTraits.mass + thrusterTraits.mass + mouthTraits.mass; 
       
        float senseDist = coreTraits.sensingRange;
        Vector2 direction = core.transform.rotation.eulerAngles;
        Vector2 shipVector = new Vector2(coreRigid.velocity.x, coreRigid.velocity.y);
        Quaternion goToAngle; //TODO: WIP
        Vector2 goToPoint = Vector2.one; //TODO: Fix this hella stupid shid
        * bruh this shit right here hella cringe,
         *  gives us a collider type instead of a raycasting type
         *
        Collider2D senseCast =
            Physics2D.OverlapCircle(curShipPos, senseDist/2);

        //END MVMNT STUFF
        */

        //Velocity and RigidBody Shiz
        Vector2 curShipPos = core.transform.localPosition;
        Rigidbody2D  coreBody = core.GetComponent<Rigidbody2D>();
        Rigidbody2D  thrusterBody = thruster.GetComponent<Rigidbody2D>();
        Rigidbody2D  mouthBody = mouth.GetComponent<Rigidbody2D>();
        Vector2 linearVel = coreBody.velocity;
        float angularVel = coreBody.angularVelocity;
        if(linearVel.magnitude > 0.01)Debug.DrawRay(new Vector2(curShipPos.x+(shipBounds[1]-shipBounds[3]), curShipPos.y+(shipBounds[0] - shipBounds[2])), linearVel*5, Color.red);

        //Sensing vibe 
        //TODO: Replace this shit with an expanding for loop, with an upper limit at senseRange bruh
        float senseRange = coreTraits.sensingRange;
        Collider2D[] senseCast =
            Physics2D.OverlapCircleAll(curShipPos, senseRange/2); //finds all colliders in a circle with a diameter = senseDist (Auto sorts by distance, shortest --> longest)
        List<Collider2D> senseList = new List<Collider2D>();
        

        for(int i = 0; i < senseCast.Length; i++)
            if(senseCast[i].attachedRigidbody == null)
                senseList.Add(senseCast[i]);

        //TODO: Fix
        //sort that flippity fracking mf'er into distance
        senseArr = senseList.ToArray();
        float[] senseDist =new float[senseArr.Length];
        int nearInd = 0;

        for (int i = 0; i < senseArr.Length; i++)
        {
            senseDist[i] = EuclidDist(curShipPos, senseArr[i].transform.position);
        }
        Array.Sort(senseDist);
        for(int j = 0; j < senseArr.Length; j++)
        {
            if (EuclidDist(curShipPos, senseArr[j].transform.position) == senseDist[0])
            {
                nearInd = j;
                break;
            }
            else
                nearInd = 0;
        }



        //Generate destination
        Vector2 destShipPos = GenerateDest(curShipPos, senseRange, nearInd);


        // if (destShipPos == Vector2.zero || destDist < mouthTraits.consumeRadius)
        // destShipPos
        float destDist = EuclidDist(destShipPos, curShipPos);
        // Debug.Log((destShipPos.x - curShipPos.x)+ ", " + (destShipPos.y - curShipPos.y));

        //Rotate to destination
        Vector2 cartesianError = new Vector2(destShipPos.x - curShipPos.x, destShipPos.y - curShipPos.y);

        //generates the theta val of the polar coord of the destination (w/ shipPos as origin) (IN DEG) cuz for us 0 = north, while for math nerds 0 = east
        float relThetaFinal = (Mathf.Rad2Deg * Mathf.Atan2(cartesianError.y, cartesianError.x)) -90;

        //For some weird reason unity is weird, this fixes it by inverting dir (clockwise = +, anticlockwise = -)
        //possible angles  -180 < theta < 180
        float thetaInit = core.transform.rotation.eulerAngles.z;
        thetaInit = thetaInit / 180;
        if(thetaInit > 1)
        {
            while (thetaInit > 0)
                thetaInit--;
        }
        else if(thetaInit < -1)
        {
            while(thetaInit < 0)
                thetaInit++;
        }
        thetaInit = thetaInit * 180;

        float deltaTheta = relThetaFinal - thetaInit;

        Debug.DrawLine(curShipPos, destShipPos, Color.cyan);
        // Debug.Log("Current: " + thetaInit + " Final: " + relThetaFinal + " Delta: " + deltaTheta);
        
        if(destDist > mouthTraits.consumeRadius)
            core.transform.rotation = Quaternion.Euler(new Vector3(0, 0, core.transform.rotation.eulerAngles.z + (deltaTheta * Time.deltaTime)));


        //Movement noncents


       // Debug.Log(coreBody.velocity + ", dist: " + destDist + ", theta: " + deltaTheta);

        //Debug shiz
        //  Debug.Log(senseCast[3].attachedRigidbody /*use '== null' for detection*/);  //This outputs the rigidbody of the 4th nearest object

         //  for (int a = 0; a < 360; a = a + 5) //Draw a circle cuz frickin unity doesn't have a command built in
          //     Debug.DrawLine(curShipPos, new Vector2((senseRange / 2 * Mathf.Cos(a)) + curShipPos.x, (senseRange / 2 * Mathf.Sin(a)) + curShipPos.y), Color.green);

        //   for(int i = 0; i < senseCast.Length; i++)
        //       Debug.DrawLine(curShipPos, senseCast[i].transform.position, Color.red); //draws a line to all detected objects

           for (int i = 0; i < nearInd; i++)
                 Debug.DrawLine(curShipPos, senseArr[i].transform.position); //draws a line to all VALID detected objects (except for the closest)
           for (int i = senseArr.Length -1; i > nearInd; i--)
                Debug.DrawLine(curShipPos, senseArr[i].transform.position); //draws a line to all VALID detected objects (except for the closest)
        //    if (senseCast[0].attachedRigidbody == null) ;

        //    Debug.Log(coreBody.velocity.magnitude); //this just gives us our velocity in DebugLog
        if (Input.GetKeyDown(UnityEngine.KeyCode.Space))
        {
            coreBody.AddForceAtPosition(new Vector2(100, 100), curShipPos);
            Debug.Log("pushed button");
        }
        //Camera Motion Stuff



        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Camera.main.transform.position += 5f * (Vector3.up * verticalAxis + Vector3.right * horizontalAxis) * Time.deltaTime; //u can prob figure these lines out
    }

    /* This function is just a tidy little warm blanket to setup the stuff in the scene
     * While the prefabs enjoy a nice cup of cocoa and look out at the window at snow while being wrapped up in a blanket
     * we instantiate them here, and clone them.
     *
     * I then set the mouth and thruster as children of the core
     * IT SHOULD BE NOTED HERE THAT THE CORE IS THE ONLY PREFAB WITH A RIGIDBODY,
     * AND THUS ACTS AS THE CENTRAL RIGIDBODY OF EACH SHIP
     * 
     * THE RIGIDBODY IS THE SOUL OF EACH SHIP, AS ONLY ANIMATE OBJECTS HAVE THEM, AND THEY ONLY GET 1.
     **/

    Vector2 GenerateDest(Vector2 curShipPos, float senseRange, int n)
    { //this is prob where things be gettin screwd up
        Vector2 destShipPos;

        if (this.senseArr.Length < 1) 
            destShipPos = new Vector2(randX(curShipPos, senseRange / 2), randY(curShipPos, senseRange / 2));
        else
            destShipPos = senseArr[n].transform.position;
        return destShipPos;
    }
    float EuclidDist(Vector2 startPos, Vector2 endPos)
    {
      float xDist = Mathf.Abs(endPos.x) - Mathf.Abs(startPos.x);
        float yDist = Mathf.Abs(endPos.y) - Mathf.Abs(startPos.y);
        return Mathf.Sqrt((xDist*xDist)+(yDist*yDist));
    }
    void SpawnShip()
    {
        core = Instantiate(corePrefab);
        mouth  = Instantiate(mouthPrefab);
        thruster = Instantiate(thrusterPrefab);

        mouth.transform.SetParent(core.transform);
        thruster.transform.SetParent(core.transform);

        /* Makes the offsets appropiate to where each component is
         * May not seem too important now, but will become vitally important later on once we add ship component grabbing
         **/
        shipBounds[0]++;
        mouth.transform.localPosition = Vector2.up * shipBounds[0];
        shipBounds[2]++;
        thruster.transform.localPosition = Vector2.down * shipBounds[2];
        core.transform.position = new Vector2(25f,25f);
    }

    /* This shit dummy thin,
     * just generates rand pos for a ship to go to in the sensing range for a ship to go to when no resources are in the sensing range
     * Idk if this generates a bug or not with positioning ig we'll see :)
     **/
    float randX(Vector2 origin, float radius)
    {
        return UnityEngine.Random.Range(origin.x * -1 * radius, origin.x * radius);

    }
    float randY(Vector2 origin, float radius)
    {
        return UnityEngine.Random.Range(origin.y * -1 * radius, origin.y * radius);
    }
}
