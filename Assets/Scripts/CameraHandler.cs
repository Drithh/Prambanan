using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    [Header("Camera")]
    private float speed = 1000f;

    public static Vector3 camPos;
    public static readonly float edgeScreen = 100f;


    void Update()
    {
        Move();

        if (camPos.x != 0) transform.position = camPos;
        camPos = transform.position;

    }

    void Move()
    {

        if (Input.mousePosition.x > Screen.width - edgeScreen)
        {
            float mousePosRightSide = (Screen.width - Input.mousePosition.x);
            if (mousePosRightSide > -1)
            {
                if (camPos.x < 640) camPos.x += speed * 50 / (mousePosRightSide > 15 ? mousePosRightSide : 15) * Time.deltaTime;
                else if (camPos.x > 640) camPos.x = 640;
            }
        }

        if (Input.mousePosition.x < edgeScreen)
        {
            if(Input.mousePosition.x > -1)
            {
                if (camPos.x > -640) camPos.x -= speed * 50 / (Input.mousePosition.x > 15 ? Input.mousePosition.x : 15) * Time.deltaTime;
                else if (camPos.x < -640) camPos.x = -640;
            }
        }

    }


}