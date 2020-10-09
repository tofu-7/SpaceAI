using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class initShip : MonoBehaviour
{
    
    [SerializeField]
    Transform mouthPrefab = default;
    [SerializeField]
    Transform thrusterPrefab = default;
    Core core = new Core();

    bool followed = true; //determines whether camera follows ship or not

    // Start is called before the first frame update
    void Start()
    {
        SpawnShip();

       /* //CAM STUFF
        if (followed == true)
        {
           // Camera.main.transform.SetParent(core);
          //  Camera.main.transform.localPosition = new Vector3(0, 0, -10);
        }
        else if (followed == false)
        {
            return;
        }
        //END CAM STUFF */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnShip()
    {
        Transform coreTransform = Core.CorePrefab.corePrefab.transform;
        Instantiate(coreTransform);

     //   mouth = Instantiate(mouthPrefab);
       // thruster = Instantiate(thrusterPrefab);

      //  mouth.SetParent(core);
      //  thruster.SetParent(core);

       // mouth.localPosition = Vector3.up * 1;
       // thruster.localPosition = Vector3.down * 1;
    }
}


