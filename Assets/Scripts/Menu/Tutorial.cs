using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Sprite[] spriteArray;
    private Vector2[] widthHeightImage;

    private Transform image;
    private GameObject Roro;
    private GameObject Timer;

    private int indexer = 0;
    private int lastIndexer = -1;
    private bool exited;

    void Start()
    {
        image = gameObject.transform.GetChild(1).transform;
        widthHeightImage = new Vector2[] { new Vector2(721, 441), new Vector2(721, 532), new Vector2(721, 459), new Vector2(721, 441), new Vector2(721, 632), new Vector2(721, 441), new Vector2(721, 325) };
        Roro = GameObject.Find("RoroSpawner");
        Timer = GameObject.Find("Timer");
        Roro.SetActive(false);
        Timer.SetActive(false);
    }

    void Update()
    {
        if(lastIndexer != indexer && indexer < spriteArray.Length)
        {
            if (indexer == 4)
            {
                Roro.SetActive(true);
            }
            else if (indexer == 5)
            {
                Timer.SetActive(true);
            }

            StartCoroutine(EntranceTutorialAnim());
            lastIndexer = indexer;
        }
    }

    public void ExitUI()
    {
        StartCoroutine(ExitTutorialAnim());
        exited = true;
    }

    IEnumerator EntranceTutorialAnim()
    {

        GetComponent<CanvasGroup>().alpha = 0f;
        image.GetComponent<Image>().sprite = spriteArray[indexer];
        image.GetComponent<Image>().rectTransform.sizeDelta = widthHeightImage[indexer];

        // transform.GetChild(0).transform.position = new Vector3( (widthHeightImage[indexer].x / 2f), 730f, 0f);
        for (float scaleUI = 0; scaleUI < 0.51f; scaleUI += 0.05f)
        {
            GetComponent<CanvasGroup>().alpha += 0.1f;
            transform.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSeconds(0.01f);
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(6f);
        if(exited)
        {
            exited = false;
        }
        else
        {
            StartCoroutine(ExitTutorialAnim());
        }

    }

    IEnumerator ExitTutorialAnim()
    {
        while (GetComponent<CanvasGroup>().alpha > 0)
        {
            GetComponent<CanvasGroup>().alpha -= 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(4f);
        indexer++;
    }
    

}
