using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using UnityEditor;

//TODO Comment ur damn code scoot

public class initShip : MonoBehaviour
{
    [SerializeField]
    Transform corePrefab = default;
    [SerializeField]
    Transform mouthPrefab = default;
    [SerializeField]
    Transform thrusterPrefab = default;

    Transform core;
    Transform mouth;
    Transform thruster;

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
            Camera.main.transform.SetParent(core);
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
        sumMass = coreTraits.mass + thrusterTraits.mass + mouthTraits.mass;
        Vector2 curShipPos = core.localPosition;
        float senseDist = coreTraits.sensingRange;
        Vector2 direction = core.rotation.eulerAngles;
        Quaternion goToAngle;
        Vector2 goToPoint = Vector2.one;
        RaycastHit2D senseCast =
            Physics2D.CircleCast(curShipPos, senseDist/2, direction, senseDist);

        if(senseCast.collider != null)
        {
            Debug.DrawRay(curShipPos, new Vector2(0, 1*senseCast.distance));

            goToPoint = senseCast.point;
            goToAngle = Quaternion.FromToRotation(curShipPos, goToPoint);
            core.transform.rotation = goToAngle;

            Debug.Log("hit somethin!");
        }
        else if(senseCast.collider == null)
        {
            Debug.DrawRay(curShipPos, senseCast.point);
            Debug.Log("Nothing hit");

            goToPoint = new Vector3(randX(curShipPos, senseDist), randY(curShipPos, senseDist));
            goToAngle = Quaternion.FromToRotation(curShipPos, goToPoint);
            core.transform.rotation = goToAngle;
        }
        if (Vector2.Distance(curShipPos, goToPoint) > mouthTraits.consumeRadius)
        {
            core.transform.position +=
                 new Vector3(0f, thrusterCount * (thrusterTraits.acc * sumMass), 0f);
        }
        else
        {
            EditorApplication.isPaused = true;
        }

        //END MVMNT STUFF

        //Camera Motion Stuff
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        float camSpeed = 5f - (mouthCount + thrusterCount/4);
        Camera.main.transform.position += (Vector3.up * verticalAxis * camSpeed + Vector3.right * horizontalAxis * camSpeed) * Time.deltaTime;
    }

    void SpawnShip()
    {
        core = Instantiate(corePrefab);
        mouth = Instantiate(mouthPrefab);
        thruster = Instantiate(thrusterPrefab);

        mouth.SetParent(core);
        thruster.SetParent(core);

        mouth.localPosition = Vector2.up * 1;
        thruster.localPosition = Vector2.down * 1;
        core.position = new Vector2(25f,25f);
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
