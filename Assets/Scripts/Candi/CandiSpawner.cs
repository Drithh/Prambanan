using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CandiSpawner : MonoBehaviour
{
    public GameObject[] candiFigure;

    public static int[] totalSpawnBlock = new int[] { 0, 0 };

    private float spawnRate = 1f;
    private Vector2 whereCandiSpawn;
    private float nextSpawn = 0f;
    private int figureRandomizer;

    void Update()
    {
        if (Time.time > nextSpawn && totalSpawnBlock.Sum() > 0)
        {
            nextSpawn = Time.time + spawnRate;
            whereCandiSpawn = new Vector2(Random.Range(-1200f, 1200f), transform.position.y);

            do
            {
                figureRandomizer = Random.Range(0, 2);
            } while (totalSpawnBlock[figureRandomizer] < 1);

            totalSpawnBlock[figureRandomizer]--;
            GameObject spawnedPrefab = Instantiate(candiFigure[figureRandomizer], whereCandiSpawn, Quaternion.identity);
            spawnedPrefab.transform.SetParent(GameObject.FindGameObjectWithTag("CandiBlock").transform, false);
        }
    }
}
