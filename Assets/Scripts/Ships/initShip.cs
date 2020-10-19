using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using UnityEditor;

//TODO Comment ur damn code scoot

public class initShip : MonoBehaviour
{
    [SerializeField]
    GameObject corePrefab;
    [SerializeField]
    GameObject mouthPrefab;
    [SerializeField]
    GameObject thrusterPrefab;

    GameObject core;
    GameObject mouth;
    GameObject thruster;

    CoreTraits coreTraits = new CoreTraits();
    ThrusterTraits thrusterTraits = new ThrusterTraits();
    MouthTraits mouthTraits = new MouthTraits();
    int sumMass = 0;
    int mouthCount =1;
    int thrusterCount =1;
    
    bool followed = true; //determines whether camera follows ship or not
   
    // Start is called before the first frame update
    void Start()
    {

        SpawnShip();

        //CAM STUFF
        if (followed == true)
        {
            Camera.main.transform.SetParent(core.transform);
            Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        }
        else if (followed == false)
        {
            return;
        }
        //END CAM STUFF 

    }

    // Update is called once per frame
    void Update()
    {
        //MVMNT STUFF V
        Rigidbody2D coreRigid = core.GetComponent<Rigidbody2D>();

        sumMass = coreTraits.mass + thrusterTraits.mass + mouthTraits.mass;
        Vector2 curShipPos = core.transform.localPosition;
        float senseDist = coreTraits.sensingRange;
        Vector2 direction = core.transform.rotation.eulerAngles;
        Vector2 shipVector = new Vector2(coreRigid.velocity.x, coreRigid.velocity.y);
        Quaternion goToAngle;
        Vector2 goToPoint = Vector2.one;
        Collider2D senseCast =
            Physics2D.OverlapCircle(curShipPos, senseDist/2);

        //END MVMNT STUFF

        //Camera Motion Stuff
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        Camera.main.transform.position += (Vector3.up * verticalAxis + Vector3.right * horizontalAxis) * Time.deltaTime;
    }

    void SpawnShip()
    {
        core = Instantiate(corePrefab);
        mouth  = Instantiate(mouthPrefab);
        thruster = Instantiate(thrusterPrefab);

        mouth.transform.SetParent(core.transform);
        thruster.transform.SetParent(core.transform);

        mouth.transform.localPosition = Vector2.up * 1;
        thruster.transform.localPosition = Vector2.down * 1;
        core.transform.position = new Vector2(25f,25f);
    }
    float randX(Vector2 origin, float senseDist)
    {
        return Random.Range(origin.x * -1 * senseDist, origin.x * senseDist);

    }
    float randY(Vector2 origin, float senseDist)
    {
        return Random.Range(origin.y * -1 * senseDist, origin.y * senseDist);
    }
}


