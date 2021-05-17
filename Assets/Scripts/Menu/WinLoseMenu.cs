using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinLoseMenu : PauseMenu
{
    public Sprite[] spriteArray;
    public static int condition;
    public static int loseTo = -1;

    public static bool askExit;

    private Image gameMessage;
    private Transform gameFinished;
    private Vector2[] widthHeightImage = new Vector2[] { new Vector2(721, 612), new Vector2(721, 459) };

    private void Awake()
    {
        Pause();
        transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = spriteArray[condition];
        transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = spriteArray[condition + 2];


        gameMessage = transform.GetChild(1).transform.GetChild(5).GetComponent<Image>();
        gameFinished = transform.GetChild(1).GetChild(6);

        gameFinished.gameObject.SetActive(false);

        if (loseTo != -1)
        {
            StartCoroutine(GameMessage(loseTo));
            gameMessage.GetComponent<Image>().rectTransform.sizeDelta = widthHeightImage[loseTo];
            loseTo = -1;
        }
        gameMessage.GetComponent<CanvasGroup>().alpha = 0f;
    }
    private void Update()
    {
        if(askExit)
        {
            askExit = false;
            Resume();
        }
    }
    public override void Resume()
    {
        StartCoroutine(ExitUi());
        Time.timeScale = 1f;
    }

    public void LoadLevel()
    {

        if (condition == 1)
        {
            if (GameSceneManager.playerLevel == 5)
            {
                StartCoroutine(GameFinished());
                return;
            }
            else
            {
                GameSceneManager.nextLevel = true;
            }
        }
        else
        {
            GameSceneManager.loadLevel = true;
        }
        Resume();
    }

    IEnumerator GameFinished()
    {
        gameFinished.gameObject.SetActive(true);
        for (float scaleUI = 0; scaleUI < 0.65f; scaleUI += 0.05f)
        {
            gameFinished.GetComponent<CanvasGroup>().alpha += 0.1f;
            gameFinished.transform.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        gameFinished.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        yield return new WaitForSecondsRealtime(6f);


        while (gameFinished.GetComponent<CanvasGroup>().alpha > 0)
        {
            gameFinished.GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    IEnumerator GameMessage(int spriteIndex)
    {
        yield return new WaitForSecondsRealtime(1f);

        gameMessage.sprite = spriteArray[spriteIndex + 4];
        for (float scaleUI = 0; scaleUI < 0.51f; scaleUI += 0.05f)
        {
            gameMessage.GetComponent<CanvasGroup>().alpha += 0.1f;
            gameMessage.transform.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        gameMessage.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        yield return new WaitForSecondsRealtime(6f);

        while (gameMessage.GetComponent<CanvasGroup>().alpha > 0)
        {
            gameMessage.GetComponent<CanvasGroup>().alpha -= 0.1f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }

    IEnumerator ExitUi()
    {
        while (pauseMenuUI.GetComponent<CanvasGroup>().alpha > 0)
        {
            pauseMenuUI.GetComponent<CanvasGroup>().alpha -= 0.1f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        SceneManager.UnloadSceneAsync("WinLoseMenu");

    }
}

