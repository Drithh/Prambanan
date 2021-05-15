using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private float speed = 50f;
    private float mousePosSide;
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
            mousePosSide = edgeScreen - (Screen.width - Input.mousePosition.x);
            if (mousePosSide < 120)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x + (speed * (mousePosSide > 80 ? 80 : mousePosSide) * Time.deltaTime), -640f, 640f), transform.position.y, transform.position.z);
            }
        }

        if (Input.mousePosition.x < edgeScreen)
        {
            mousePosSide = (edgeScreen - Input.mousePosition.x);
            if (mousePosSide < 120)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x - (speed * (mousePosSide > 80 ? 80 : mousePosSide) * Time.deltaTime), -640f, 640f), transform.position.y, transform.position.z);
            }
        }
    }

}