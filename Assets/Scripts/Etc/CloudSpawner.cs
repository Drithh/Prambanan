using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudObject;
    public static int totalCloud = 15;

    private void Awake()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnCloud(Random.Range(-13f, 13f));
        }
    }

    void Update()
    {
        if (totalCloud < 15)
        {
            totalCloud++;
            SpawnCloud(Random.Range(0, 2) == 1 ? 13 : -13);
        }
    }

    void SpawnCloud(float cloudPosX)
    {
        float speed;
        do
        {
            speed = Random.Range(-3, 3) * 10;
        } while (speed == 0);

        Vector3 whereCloudSpawn = new Vector3(cloudPosX, Random.Range(2f, 3f), 1);
        if (speed > 0 && whereCloudSpawn.x > 0)
        {
            whereCloudSpawn.x *= -1f;
        }

        GameObject cloud = Instantiate(cloudObject, whereCloudSpawn, Quaternion.identity);
        cloud.transform.SetParent(GameObject.FindGameObjectWithTag("CloudSpawner").transform, false);

        cloud.transform.localScale = new Vector2(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f));
        cloud.GetComponent<CloudObject>().startSpawn(speed);

    }




}
