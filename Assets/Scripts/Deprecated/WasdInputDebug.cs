using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WasdInputDebug : MonoBehaviour
{
    ShipScript shipScript;
    Thruster[] _t;
    GameObject co;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool wtrig = Input.GetKey("w");
        bool atrig = Input.GetKey("a");
        bool strig = Input.GetKey("s");
        bool dtrig = Input.GetKey("d");
        bool[] trigList = { wtrig, atrig, strig, dtrig };

		//Debug.Log("(" + trigList[0] + ", " + trigList[1] + ", " + trigList[2] + ", " + trigList[3] + ")");

    }
    public void UpdateThrusters(Thruster[] thrust) {
        _t = thrust;
    }

    public void UpdateCore(GameObject go, Rigidbody2D rb2d)
	{
        co = go;
        rb = rb2d;
	}
}
