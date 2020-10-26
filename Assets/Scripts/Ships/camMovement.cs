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
      mainCamera = GetComponent<Camera>();
      startingFOV = mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
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

      float scroll = Input.GetAxis("Mouse ScrollWheel");

      currentFOV += scroll * zoomRate;

      currentFOV = Mathf.Clamp(currentFOV, minFOV, maxFOV);
      mainCamera.fieldOfView = currentFOV;
    }
}
