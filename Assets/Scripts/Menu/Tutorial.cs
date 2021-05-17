using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal; 

public class Tutorial : MonoBehaviour
{
    public Sprite[] spriteArray;
    private Vector2[] widthHeightImage = new Vector2[] { new Vector2(721, 441), new Vector2(721, 532), new Vector2(721, 459), new Vector2(721, 441), new Vector2(721, 632), new Vector2(721, 441), new Vector2(721, 325) };

    private GameObject roro;
    private GameObject timer;
    private GameObject blueprint1;
    private GameObject redSide;
    private GameObject tutorialCandi;

    private Transform image;
    private CameraHandler cameraHandler;
    private Light2D moonLight;

    public static bool cameraMoved = false;

    private int indexer = 0;
    private int lastIndexer = -1;
    private bool exited;
    private float currentMoonLight;

    void Start()
    {

        roro        = GameObject.Find("RoroSpawner");
        timer       = GameObject.Find("Timer");
        blueprint1  = GameObject.Find("Blueprint1");
        redSide     = GameObject.Find("RedSide");
        tutorialCandi = GameObject.Find("TutorialCandi");

        image = gameObject.transform.GetChild(1).transform;
        moonLight = GameObject.Find("MoonLight").GetComponent<Light2D>();
        cameraHandler = GameObject.Find("MainCamera1").GetComponent<CameraHandler>();




        currentMoonLight = moonLight.pointLightOuterRadius;
        moonLight.pointLightOuterRadius *= 2;

        Debug.Log(cameraHandler.name);

        cameraHandler.enabled = false;

        redSide.SetActive(false);
        blueprint1.SetActive(false);
        roro.SetActive(false);
        timer.SetActive(false);

    }

    void Update()
    {
        if (lastIndexer != indexer && indexer < spriteArray.Length)
        {
            switch(indexer)
            {
                case 3:
                    cameraHandler.enabled = true;
                    StartCoroutine(RedSideBlink());
                    break;
                case 4:
                    roro.SetActive(true);
                    break;
                case 5:
                    timer.SetActive(true);
                    break;
                case 6:
                    blueprint1.SetActive(true);
                    StartCoroutine(ChangeMoonLight());
                    break;
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



    IEnumerator RedSideBlink()
    {
        redSide.SetActive(true);
        Debug.Log(redSide.activeSelf);
        CanvasGroup redOpacity = redSide.GetComponent<CanvasGroup>();
        while (cameraHandler.transform.position != new Vector3(0, 0, -90))
        {
            while (redOpacity.alpha > 0)
            {
                redOpacity.alpha -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
            while (redOpacity.alpha < 1)
            {
                redOpacity.alpha += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        } 
        redSide.SetActive(false); ;

    }

    IEnumerator ChangeMoonLight()
    {
        StartCoroutine(DestroyCandi());
        while (moonLight.pointLightOuterRadius > currentMoonLight)
        {
            moonLight.pointLightOuterRadius -= 1;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
        
    }

    IEnumerator DestroyCandi()
    {
        tutorialCandi.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        for (float scaleUI = 1; scaleUI > 0f; scaleUI -= 0.05f)
        {
            tutorialCandi.GetComponent<CanvasGroup>().alpha -= 0.1f;
            tutorialCandi.transform.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(tutorialCandi);
    }

    IEnumerator EntranceTutorialAnim()
    {

        GetComponent<CanvasGroup>().alpha = 0f;
        image.GetComponent<Image>().sprite = spriteArray[indexer];
        image.GetComponent<Image>().rectTransform.sizeDelta = widthHeightImage[indexer];

        switch (indexer)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                transform.localPosition = new Vector3(300, 0, 0);
                break;
            case 4:
                transform.localPosition = new Vector3(-300, 100, 0);
                break;
            case 5:
            case 6:
                transform.localPosition = new Vector3(0, 50, 0);
                break;
        }

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
