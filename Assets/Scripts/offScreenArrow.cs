using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offScreenArrow : MonoBehaviour
{
  //Our object
    public GameObject core;

    void Update()
    {
        if(core.GetComponent<Renderer>().isVisible == false)
        {
          //Getting the screen center so I can use the angle between the center of the screen and the position of the ship to get the rotation of the arrow
            Vector3 screenCenter = new Vector3(Screen.width, Screen.height,0) /2;

          //This is used to get the position of the ship
            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(core.transform.position);

          //Finding the direction the ship is relative to the screen center, and we use normalized because we want it to be less than 1 so we can convert it into degrees
            Vector3 dir = (targetPositionScreenPoint - screenCenter).normalized;

          //Getting the angle the arrow should point
            float angle = Mathf.Atan2(dir.y,dir.x);

          //Setting the position of the arrow to match the position of the target relative to the center of the screen
            transform.position = screenCenter + new Vector3(Mathf.Cos(angle) * screenCenter.x, Mathf.Sin(angle) * screenCenter.y, 0);

          //Setting the angle of the arrow to rotate around the center of the screen and point towards the target
            transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
        else
        {
          transform.position = new Vector3(3000,3000,0);
        }
    }
}
