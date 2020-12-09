using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirect : MonoBehaviour
{

    private Vector2 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        var z = Random.Range(min, max);
        return new Vector2(x, y);
    }

    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody>();  // when the simulation starts it will go in random direction, but need to add for when it does not detect anything in the cast...
        rb.velocity = RandomVector(0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
