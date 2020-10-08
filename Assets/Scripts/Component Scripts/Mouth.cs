using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouthTraits
{
    topValid = 0,
    leftValid = 1,
    BottomValid = 1,
    RightValid = 1,
    mass = 1,
    consumeRadius =  (int)GlobalEnvironmentLib.TileSize * 1,
    consumeMaxVel = 2, //in tiles per second
    consumeRate = 1 //in resource unit per second
}
public class Mouth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
