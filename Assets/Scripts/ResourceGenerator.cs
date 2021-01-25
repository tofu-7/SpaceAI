using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Security.Cryptography;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{

    public static int ResourceCap = 100;
    public Transform resource;

    // Start is called before the first frame update
    void Start() 
    {
        Transform resc;
        for (int i = 0; i < ResourceCap; i++)
        {
            Transform newR = resource.GetComponent<Transform>();
            newR.position = new Vector3(Random.Range(0f, GlobalEnvironmentLib.xBound), Random.Range(0f, GlobalEnvironmentLib.yBound), 0);

            Collider2D[] rCast =
                Physics2D.OverlapCircleAll(newR.position, 20);
            List<Collider2D> rList = new List<Collider2D>();

            bool validPlace = true;

            for (int j = 0; j < rCast.Length; j++) {
                float offsetDist = EuclidDist( newR.position, rCast[j].transform.position);
                if (offsetDist < 1)
                    validPlace = false;
            }

            if (validPlace == true) 
            { 
                resc = Instantiate(resource);
                resc.gameObject.layer = LayerMask.NameToLayer("Resc");
            }
            else
            {
                ResourceCap++;
                validPlace = true;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    float EuclidDist(Vector2 startPos, Vector2 endPos)
    {
        float xDist = Mathf.Abs(endPos.x) - Mathf.Abs(startPos.x);
        float yDist = Mathf.Abs(endPos.y) - Mathf.Abs(startPos.y);
        return Mathf.Sqrt((xDist * xDist) + (yDist * yDist));
    }
}
