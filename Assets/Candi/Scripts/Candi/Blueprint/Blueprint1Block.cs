using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint1Block : MonoBehaviour
{
    private void Start()
    {
        CandiSpawner.totalSpawnBlock[0] += 11;
        CandiSpawner.totalSpawnBlock[1] += 7;
    }
}
