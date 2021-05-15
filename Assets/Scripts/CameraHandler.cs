using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private float speed = 1000f;

    public static readonly float edgeScreen = 120f;
    public static Vector3 camPos;

    void Update()
    {
        Move();
        camPos = transform.position;
    }

    void Move()
    {

        if (Input.mousePosition.x > Screen.width - edgeScreen)
        {
            float mousePosRightSide = (Screen.width - Input.mousePosition.x);
            if (mousePosRightSide > -1)
            {
                transform.position = new Vector3 (Mathf.Clamp(transform.position.x + (speed * 100 / (mousePosRightSide > 15 ? mousePosRightSide : 15) * Time.deltaTime) , -640, 640) , transform.position.y, transform.position.z);
            }
        }

        if (Input.mousePosition.x < edgeScreen)
        {
            if(Input.mousePosition.x > -1)
            {
                transform.position = new Vector3 (Mathf.Clamp(transform.position.x - (speed * 100 / (Input.mousePosition.x > 15 ? Input.mousePosition.x : 15) * Time.deltaTime) , -640, 640), transform.position.y, transform.position.z);
            }
        }

    }


}