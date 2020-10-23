using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipVision : MonoBehaviour
{

    float visionRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Collider2D nearestResource(Transform ship)
    {
        Collider2D[] things = Physics2D.OverlapCircleAll();
    }
}
