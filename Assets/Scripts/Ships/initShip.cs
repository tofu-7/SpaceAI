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
* cuz I be pulling an ori rn :( 
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
        Rigidbody2D coreRigid = core.GetComponent<Rigidbody2D>();
            
        //AAAHHHHHHHHH
        sumMass = coreTraits.mass + thrusterTraits.mass + mouthTraits.mass; 
        Vector2 curShipPos = core.transform.localPosition;
        float senseDist = coreTraits.sensingRange;
        Vector2 direction = core.transform.rotation.eulerAngles;
        Vector2 shipVector = new Vector2(coreRigid.velocity.x, coreRigid.velocity.y);
        Quaternion goToAngle; //TODO: WIP
        Vector2 goToPoint = Vector2.one; //TODO: Fix this hella stupid shid
        /* bruh this shit right here hella cringe,
         *  gives us a collider type instead of a raycasting type
         **/
        Collider2D senseCast =
            Physics2D.OverlapCircle(curShipPos, senseDist/2); 

        //END MVMNT STUFF

        //Camera Motion Stuff
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Camera.main.transform.position += (Vector3.up * verticalAxis + Vector3.right * horizontalAxis) * Time.deltaTime; //u can prob figure these lines out
    }

    /* This function is just a tidy little warm blanket to setup the stuff in the scene
     * While the prefabs enjoy a nice cup of cocoa and look out at the window at snow while being wrapped up in a blanket
     * we instantiate them here, and clone them.
     * 
     * I then set the mouth and thruster as children of the core
     * IT SHOULD BE NOTED HERE THAT THE CORE IS THE ONLY PREFAB WITH A RIGIDBODY, 
     * AND THUS ACTS AS THE CENTRAL RIGIDBODY OF EACH SHIP
     **/
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
        mouth.transform.localPosition = Vector2.up * 1;
        thruster.transform.localPosition = Vector2.down * 1;
        core.transform.position = new Vector2(25f,25f);
    }
    
    /* This shit dummy thin, 
     * just generates rand pos for a ship to go to in the sensing range for a ship to go to when no resources are in the sensing range
     * Idk if this generates a bug or not with positioning ig we'll see :)
     **/
    float randX(Vector2 origin, float senseDist)
    {
        return Random.Range(origin.x * -1 * senseDist, origin.x * senseDist);

    }
    float randY(Vector2 origin, float senseDist)
    {
        return Random.Range(origin.y * -1 * senseDist, origin.y * senseDist);
    }
}


