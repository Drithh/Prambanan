using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private float speed = 50f;
    private float mousePosSide;
    public static readonly float edgeScreen = 120f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        float mousePosScaledX = Input.mousePosition.x * GameSceneManager.screenScale;
        float mousePosScaledY = Input.mousePosition.y * GameSceneManager.screenScale;
        float screenWidthScaled = Screen.width * GameSceneManager.screenScale;
        float screenHeightScaled = Screen.height * GameSceneManager.screenScale;

        if (mousePosScaledY > screenHeightScaled - edgeScreen)
        {
            mousePosSide = edgeScreen - (screenHeightScaled - mousePosScaledY);
            transform.position = new Vector3(transform.position.x , Mathf.Clamp(transform.position.y + (speed * (mousePosSide > 20 ? 20 : mousePosSide) * Time.deltaTime), 0f, 200f), transform.position.z);
        }

        if (mousePosScaledY < edgeScreen)
        {
            mousePosSide = (edgeScreen - mousePosScaledY);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y - (speed * (mousePosSide > 20 ? 20 : mousePosSide) * Time.deltaTime), 0f, 200f), transform.position.z);
        }

        if (mousePosScaledX > screenWidthScaled - edgeScreen)
        {
            mousePosSide = edgeScreen - (screenWidthScaled - mousePosScaledX);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + (speed * (mousePosSide > 40 ? 40 : mousePosSide) * Time.deltaTime), -640f, 640f), transform.position.y, transform.position.z);
        }


        if (mousePosScaledX < edgeScreen)
        {
            mousePosSide = (edgeScreen - mousePosScaledX);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x - (speed * (mousePosSide > 40 ? 40 : mousePosSide) * Time.deltaTime), -640f, 640f), transform.position.y, transform.position.z);
        }
    }

}