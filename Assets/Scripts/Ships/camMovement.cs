using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour
{
  private Camera mainCamera;
  private float startingFOV;
  public float minFOV;
  public float maxFOV;
  public float zoomRate;

  private float currentFOV;
    // Start is called before the first frame update
    void Start()
    {
      //set camera as variable and set the starting fov to how the camera looks
      //when the scene starts
      mainCamera = GetComponent<Camera>();
      startingFOV = mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
      //keeps track of the current field of view
      currentFOV = mainCamera.fieldOfView;

      useWheel();

    /*  if(Input.GetKey(KeyCode.I))
      {
        currentFOV -= zoomRate;
      }

      if(Input.GetKey(KeyCode.O))
      {
        currentFOV += zoomRate;
      }
      currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
      mainCamera.fieldOfView = currentFOV;
      */
    }

    public void useWheel()
    {

      currentFOV = mainCamera.fieldOfView;
      //using GetAxis for direction of scroll wheel
      float scroll = Input.GetAxis("Mouse ScrollWheel");
      //applying the scroll wheel to the current fov so it actually moves
      currentFOV += scroll * zoomRate;
      //keep em in bounds of min-max
      currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
      mainCamera.fieldOfView = currentFOV;
    }
}
