using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiSpawner : MonoBehaviour
{
    public GameObject candi;
    public float spawnRate = 0.5f;

    private float randX;
    private Vector2 whereCandiSpawn;
    private float nextSpawn = 0f;
    private int max = 10;
    private RectTransform canvas;

    void Update()
    {
        if(Time.time > nextSpawn && max > 0)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-1200f, 1200f);
            whereCandiSpawn = new Vector2(randX, transform.position.y);

            GameObject spawnedPrefab = Instantiate(candi, whereCandiSpawn, Quaternion.identity) as GameObject;
            spawnedPrefab.transform.SetParent(GameObject.FindGameObjectWithTag("CandiBlock").transform, false);
            max--;
        }
    }
}
