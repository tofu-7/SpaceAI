using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public int enum CoreTraits
=======
public enum CoreTraits
>>>>>>> Stashed changes
{
    offspringCount = 1,
    offspringMutationChance = 15, //0 to 100
    sensingRange = (int)GlobalEnvironmentLib.TileSize * 5,
    mass = 1,
    initStorageCap = 10
}

public class Core : MonoBehaviour
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
