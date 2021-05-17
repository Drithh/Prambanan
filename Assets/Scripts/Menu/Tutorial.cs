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
    private GameObject candiSpawner;

    private Transform image;
    private CameraHandler cameraHandler;
    private Light2D moonLight;

    public static bool cameraMoved = false;

    private int indexer = 0;
    private int lastIndexer = -1;
    private bool exited;
    private float currentMoonLight;
    private bool instructionClosed;

    void Awake()
    {
        roro        = GameObject.Find("RoroSpawner");
        timer       = GameObject.Find("Timer");
        blueprint1  = GameObject.Find("Blueprint1s");
        redSide     = GameObject.Find("RedSide");
        tutorialCandi = GameObject.Find("TutorialCandi");
        candiSpawner = GameObject.Find("CandiSpawner");

        image = gameObject.transform.GetChild(1).transform;
        moonLight = GameObject.Find("MoonLight").GetComponent<Light2D>();
        cameraHandler = GameObject.Find("MainCamera1").GetComponent<CameraHandler>();




        currentMoonLight = moonLight.pointLightOuterRadius;
        moonLight.pointLightOuterRadius *= 2;


        cameraHandler.enabled = false;

        redSide.SetActive(false);
        candiSpawner.SetActive(false);
        blueprint1.SetActive(false);
        roro.SetActive(false);
        timer.SetActive(false);

        StartCoroutine(OpenInstruction());
    }

    void Update()
    {
        if (lastIndexer != indexer && indexer < spriteArray.Length && instructionClosed)
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
                    StartCoroutine(EnableEnemy());
                    break;
                case 6:
                    StartCoroutine(ChangeMoonLight());
                    break;
            }
            StartCoroutine(EntranceTutorialAnim());
            lastIndexer = indexer;
        }
    }

    public void CloseInstructionButton()
    {
        instructionClosed = true;
        StartCoroutine(CloseInstruction());
    }

    public void ExitUI()
    {
        StartCoroutine(ExitTutorialAnim());
        exited = true;
    }

    IEnumerator EnableEnemy()
    {
        candiSpawner.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        blueprint1.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        timer.SetActive(true);
    }


    IEnumerator OpenInstruction()
    {
        Time.timeScale = 0f;
        Transform instruction = transform.GetChild(2);
        for (float scaleUI = 0; scaleUI < 0.9f; scaleUI += 0.09f)
        {
            instruction.GetComponent<CanvasGroup>().alpha += 0.1f;
            instruction.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        instruction.localScale = new Vector3(0.85f, 0.85f, 0.85f);
        instruction.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    IEnumerator CloseInstruction()
    {
        Transform instruction = transform.GetChild(2);
        for (float scaleUI = 0.8f; scaleUI > 0f; scaleUI -= 0.08f)
        {
            instruction.GetComponent<CanvasGroup>().alpha -= 0.1f;
            instruction.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Time.timeScale = 1f;
    }

    IEnumerator RedSideBlink()
    {
        redSide.SetActive(true);
        CanvasGroup redOpacity = redSide.GetComponent<CanvasGroup>();
        while (true)
        {
            while (redOpacity.alpha > 0)
            {
                redOpacity.alpha -= 0.1f;
                yield return new WaitForSecondsRealtime(0.1f);
            }
            yield return new WaitForSecondsRealtime(0.5f);
            if(cameraHandler.transform.position != new Vector3(0, 0, -90))
            {
                redSide.SetActive(false);
                yield break;
            }
            while (redOpacity.alpha < 1)
            {
                redOpacity.alpha += 0.1f;
                yield return new WaitForSecondsRealtime(0.1f);
            }
        } 

    }

    IEnumerator ChangeMoonLight()
    {
        StartCoroutine(DestroyCandi());
        while (moonLight.pointLightOuterRadius > currentMoonLight)
        {
            moonLight.pointLightOuterRadius -= 1;
            yield return new WaitForSecondsRealtime(0.01f);
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
            yield return new WaitForSecondsRealtime(0.01f);
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
            yield return new WaitForSecondsRealtime(0.01f);
        }
        transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSecondsRealtime(6f);
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
            yield return new WaitForSecondsRealtime(0.05f);
        }
        yield return new WaitForSecondsRealtime(4f);
        indexer++;
    }
    

}
