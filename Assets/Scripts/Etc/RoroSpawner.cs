using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoroSpawner : MonoBehaviour
{
    public GameObject roroObject;

    private string[] keyArray = { "a", "s", "d", "q", "w", "e" };
    public static int[] keySummoned;
    public Sprite[] spriteArray;

    public float spawnRate = 1f;
    private float nextSpawn;
    int keyRandomizer;

    private void Start()
    {
        keySummoned =  new int[6] { 0, 0, 0, 0, 0, 0 };
        nextSpawn = 0f;
        RoroObject.maxTime = spawnRate * 6f;
    }

    void Update()
    {
        keyRandomizer = Random.Range(0, 6);
        if (keySummoned[keyRandomizer] == 1) return;

        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnRoro();
        }
    }

    void SpawnRoro()
    {
        int spawnRandomizer = Random.Range(0, 2);
        


        string key = keyArray[keyRandomizer];
        keySummoned[keyRandomizer] = 1;

        GameObject roro = Instantiate(roroObject);
        RoroObject roros = roro.GetComponent<RoroObject>();

        roros.transform.SetParent(GameObject.FindGameObjectWithTag("RoroSpawner").transform, false);
        roros.transform.SetSiblingIndex(0);

        roros.Keyrandomized = keyRandomizer;
        roros.destroyKey = key;

        roros.transform.GetChild(0).GetComponent<Text>().text = key.ToUpper();
        roros.transform.GetChild(0).localPosition = new Vector3(spawnRandomizer == 0 ? -10 : 60, 0, 0);

        roros.GetComponent<Image>().sprite = spriteArray[spawnRandomizer];
        
        roros.GetComponent<Transform>().localPosition = new Vector3(spawnRandomizer == 1 ? -985 : 985, Random.Range(2f, 3f), 1);

    }




}
