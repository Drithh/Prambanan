using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float startPosX, startPosY, distanceX, distanceY;
    public float parallaxEffect;

    private void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

    }
    private void Update()
    {
        distanceX = Camera.main.GetComponent<Camera>().transform.position.x * parallaxEffect;
        distanceY = Camera.main.GetComponent<Camera>().transform.position.y * parallaxEffect;
        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);
    }
}
