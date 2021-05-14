using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public static class ExtensionMethods
{
    public static Transform[] FindChildren(this Transform transform, string name)
    {
        return transform.GetComponentsInChildren<Transform>().Where(t => t.name == name).ToArray();
    }
}

public class Blueprint1Block : MonoBehaviour
{

    private void FindAllChildsName(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.name.Contains("Square"))
            {
                CandiSpawner.totalSpawnBlock[1] += 1;
            }
            else
            {
                CandiSpawner.totalSpawnBlock[0] += 1;
            }
            FindAllChildsName(child);
        }
    }

    void Start()
    {
        FindAllChildsName(transform);
    }
}


