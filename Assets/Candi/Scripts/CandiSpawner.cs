using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiSpawner : MonoBehaviour
{
    public GameObject candi;
    public float spawnRate = 2f;

    private float randX;
    private Vector2 whereCandiSpawn;
    private float nextSpawn = 0f;

    private RectTransform canvas;

    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-600f, 600f);
            whereCandiSpawn = new Vector2(randX, transform.position.y);

            GameObject spawnedPrefab = Instantiate(candi, whereCandiSpawn, Quaternion.identity) as GameObject;
            spawnedPrefab.transform.SetParent(GameObject.FindGameObjectWithTag("CandiBlock").transform, false);
        }
    }
}
