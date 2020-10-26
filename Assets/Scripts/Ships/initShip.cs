using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using UnityEditor;

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
    float senseDist;
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
        Vector2 linearVel = coreBody.velocity;
        float angularVel = coreBody.angularVelocity;
        if(linearVel.magnitude > 0.01)Debug.DrawRay(new Vector2(curShipPos.x+(shipBounds[1]-shipBounds[3]), curShipPos.y+(shipBounds[0] - shipBounds[2])), linearVel*5, Color.red);

        //Sensing vibe
        senseDist = coreTraits.sensingRange;
        Collider2D[] senseCast =
            Physics2D.OverlapCircleAll(curShipPos, senseDist/2); //finds all colliders in a circle with a diameter = senseDist (Auto sorts by distance, shortest --> longest)
        List<Collider2D> senseList = new List<Collider2D>();
        

        for(int i = 0; i < senseCast.Length; i++)
            if(senseCast[i].attachedRigidbody == null)
                senseList.Add(senseCast[i]);

        //TODO: Fix
        senseList.Sort(); //sort that flippity fracking mf'er into distance
        senseArr = senseList.ToArray();
 
        //Generate destination
        Vector2 destShipPos = DestinationCalc(curShipPos);

        //Rotate to destination
        float thetaError = (360*Mathf.Atan2(destShipPos.y, destShipPos.x))/(2*Mathf.PI); //generates the theta val of the polar coord of the destination (w/ shipPos as origin) (IN DEG)
        thetaError += 90;
        Debug.Log(thetaError);
        Debug.DrawLine(curShipPos, destShipPos, Color.cyan);

        if (EuclidDist(curShipPos, destShipPos) > 2*mouthTraits.consumeRadius) //this is the loop to run until it gets close enough to c o n s u m e
        {
            //  angleError = Quaternion.Angle(core.transform.rotation, destShipPos); 
            Quaternion errorQuat = Quaternion.Euler(core.transform.rotation.x, core.transform.rotation.y, thetaError);
            core.transform.rotation = 
                new Quaternion(core.transform.rotation.x + errorQuat.x*Time.deltaTime,
                                core.transform.rotation.y +errorQuat.y * Time.deltaTime, 
                                core.transform.rotation.z + errorQuat.z * Time.deltaTime, 
                                core.transform.rotation.w + errorQuat.w * Time.deltaTime);
        }




        //Debug shiz
      //  Debug.Log(senseCast[3].attachedRigidbody /*use '== null' for detection*/);  //This outputs the rigidbody of the 4th nearest object

     //   for (int a = 0; a < 360; a = a + 1) //Draw a circle cuz frickin unity doesn't have a command built in
     //       Debug.DrawLine(curShipPos, new Vector2((senseDist / 2 * Mathf.Cos(a)) + curShipPos.x, (senseDist / 2 * Mathf.Sin(a)) + curShipPos.y), Color.green);

     //   for(int i = 0; i < senseCast.Length; i++)
     //       Debug.DrawLine(curShipPos, senseCast[i].transform.position, Color.red); //draws a line to all detected objects

        for (int i = 1; i < senseArr.Length; i++)
            Debug.DrawLine(curShipPos, senseArr[i].transform.position); //draws a line to all VALID detected objects (except for the closest)

        //    if (senseCast[0].attachedRigidbody == null) ;

       //    Debug.Log(coreBody.velocity.magnitude); //this just gives us our velocity in DebugLog
       // if (Input.GetKeyDown(UnityEngine.KeyCode.Space)) coreBody.AddForce(new Vector2(20,0));

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

    Vector2 DestinationCalc(Vector2 curShipPos)
    { //this is prob where things be gettin screwd up
        Vector2 destShipPos;

        if (this.senseArr.Length < 1) 
            destShipPos = new Vector2(randX(curShipPos, this.senseDist / 2), randY(curShipPos, senseDist / 2));
        else
            destShipPos = senseArr[0].transform.position;
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
        return Random.Range(origin.x * -1 * radius, origin.x * radius);

    }
    float randY(Vector2 origin, float radius)
    {
        return Random.Range(origin.y * -1 * radius, origin.y * radius);
    }
}
