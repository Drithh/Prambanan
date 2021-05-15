using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    private float startPos, distance;
    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
    }
    private void Update()
    {

        distance = CameraHandler.camPos.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
