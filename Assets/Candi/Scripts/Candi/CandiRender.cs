using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandiRender : MonoBehaviour
{
    public Sprite[] spriteArray;

    private void Awake()
    {
        gameObject.GetComponent<Image>().sprite = spriteArray[Random.Range(0, spriteArray.Length)];
    }

}
