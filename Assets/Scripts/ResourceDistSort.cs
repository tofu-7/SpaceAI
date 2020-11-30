using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResourceDistSort : MonoBehaviour
{

    private GameObject[] resources;
    public GameObject nearResource;

    void Update()
    {
        nearResource = NearResource();
    }

    GameObject NearResource()
    {
        resources = GameObject.FindGameObjectsWithTag("resources");
        resources = resources.OrderBy( x => Vector2.Distance(this.transform.position, x.transform.position)).ToArray();
        return resources[0];
    }
}
