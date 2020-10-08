using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    // Start is called before the first frame update

    void Start()
    {
        SpawnShip();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnShip()
    {
        core = Instantiate(corePrefab);
        mouth = Instantiate(mouthPrefab);
        thruster = Instantiate(thrusterPrefab);

        mouth.SetParent(core);
        thruster.SetParent(core);

        mouth.localPosition = Vector3.up * GlobalEnvironmentLib.TileSize;
        thruster.localPosition = Vector3.down * GlobalEnvironmentLib.TileSize;
        thruster.localRotation = new Quaternion(0, 0, 180, 0);
    }

}


