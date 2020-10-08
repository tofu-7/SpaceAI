using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Security.Cryptography;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    public static int ResourceCap = 50;
    public Transform resource;

    // Start is called before the first frame update
    void Start()
    {
        for (int i= 0; i<ResourceCap; i++)
        {
            Transform newR = Instantiate(resource).GetComponent<Transform>();
            newR.localPosition = new Vector3(10, 0, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
