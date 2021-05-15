using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudObject : MonoBehaviour
{
    private float cloudSpeed;

    public void startSpawn(float speed)
    {
        cloudSpeed = speed;
    }

    void Update()
    {
        transform.Translate(Vector3.right * (cloudSpeed * Time.deltaTime));
        if(Mathf.Abs(transform.localPosition.x) > 13)
        {
            CloudSpawner.totalCloud--;
            Destroy(gameObject);
        }
    }
}
