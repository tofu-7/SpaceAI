using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offScreenArrow : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    void Start()
    {
      targetPosition = new Vector3(0,0,0);
      pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = (Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg) % 360;

        pointerRectTransform.localEulerAngles = new Vector3(0,0,angle);
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <=0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

    }
}
