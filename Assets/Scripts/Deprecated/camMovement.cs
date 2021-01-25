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
    private float camSpeed = 14f;
    // Start is called before the first frame update
    void Start()
    {
        //set camera as variable and set the starting fov to how the camera looks
        //when the scene starts
        mainCamera = Camera.main;
        startingFOV = mainCamera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Q) && mainCamera.transform.parent != null)
        {
            mainCamera.transform.parent = null;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && mainCamera.transform.parent == null)
        {
            mainCamera.transform.parent = 
        }*/
        //keeps track of the current field of view
        currentFOV = mainCamera.fieldOfView;

        useWheel();

        /*float horizontalAxis = Input.GetAxisRaw("Mouse X");
        float verticalAxis = Input.GetAxisRaw("Mouse Y");


        if (Input.GetMouseButton(0))
        {
            mainCamera.transform.position += (Vector3.up * -verticalAxis * camSpeed + Vector3.right * -horizontalAxis * camSpeed) * Time.deltaTime;
        }*/

    }
    void FixedUpdate()
    {
        float horizontalAxis = Input.GetAxisRaw("Mouse X");
        float verticalAxis = Input.GetAxisRaw("Mouse Y");


        if (Input.GetMouseButton(0))
        {
            //Debug.Log("yeet");
            mainCamera.GetComponent<Transform>().localPosition += new Vector3 (-horizontalAxis * camSpeed, -verticalAxis * camSpeed, 0f) * Time.deltaTime;
        }

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
