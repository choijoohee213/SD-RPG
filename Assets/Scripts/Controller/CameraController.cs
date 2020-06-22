using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    public Transform Player;

    [HideInInspector]
    public bool isCanRotate = true;
    private bool isMouseDown = false;

    void Start() {
        xAngle = 0;
        yAngle = 12;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    void Update() {
        if(isCanRotate != false) {

#if(UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    FirstPoint = Input.GetTouch(0).position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height; // Y값 변화가 좀 느려서 3배 곱해줌.
 
                    // 회전값을 40~85로 제한
                    if(yAngle < -10f)
                    yAngle = -10f;
                if(yAngle > 30f)
                    yAngle = 30f;
                if(xAngle < -30f)
                    xAngle = -30f;
                if(xAngle > 40f)
                    xAngle = 40f;
 
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(yAngle, xAngle, 0.0f), Time.deltaTime * 3f);
                }
            }
#else
            // 마우스가 눌림
            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
                FirstPoint = Input.mousePosition;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
                isMouseDown = true;
            }

            // 마우스가 떼짐
            if(Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject()) {
                isMouseDown = false;
            }

            if(isMouseDown) {
                SecondPoint = Input.mousePosition;
                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                yAngle = yAngleTemp - (SecondPoint.y - FirstPoint.y) * 90 * 3f / Screen.height; // Y값 변화가 좀 느려서 3배 곱해줌.

                // 회전값을 40~85로 제한
                if(yAngle < -10f)
                    yAngle = -10f;
                if(yAngle > 30f)
                    yAngle = 30f;
                if(xAngle < -30f)
                    xAngle = -30f;
                if(xAngle > 40f)
                    xAngle = 40f;

                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(yAngle, xAngle, 0.0f), Time.deltaTime * 3f);
            }
#endif
        }
    }

}
