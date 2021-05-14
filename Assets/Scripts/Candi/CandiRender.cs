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
        gameObject.GetComponent<Image>().color = new Color32(255, 170, 170, 255);

    }

}
