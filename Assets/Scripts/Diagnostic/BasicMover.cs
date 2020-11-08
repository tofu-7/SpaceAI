using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMover : MonoBehaviour
{
    [SerializeField]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + (speed * Vector2.right.x * Time.deltaTime),
                transform.position.y + (speed* Vector2.right.y * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x + (speed * Vector2.left.x * Time.deltaTime),
                transform.position.y + (speed * Vector2.left.y * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x + (speed * Vector2.up.x * Time.deltaTime),
                transform.position.y + (speed * Vector2.up.y * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x + (speed * Vector2.down.x * Time.deltaTime),
                transform.position.y + (speed * Vector2.down.y * Time.deltaTime));
        }
    }
}
