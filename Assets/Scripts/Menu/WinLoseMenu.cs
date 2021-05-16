using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WinLoseMenu : PauseMenu
{
    public Sprite[] spriteArray;
    public static int condition;
    public static bool askExit;
    private void Awake()
    {
        Pause();
        transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = spriteArray[condition];
        transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = spriteArray[condition + 2];
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
        if(GameSceneManager.playerLevel == 4)
        {
            GameFinished();
        }
        else if (condition == 1)
        {
            GameSceneManager.nextLevel = true;
        }
        else
        {
            GameSceneManager.loadLevel = true;
        }
        Resume();
    }

    IEnumerator GameFinished()
    {
        Transform gameFinished = transform.GetChild(1).transform.GetChild(5).GetComponent<Image>().transform;
        gameFinished.GetComponent<CanvasGroup>().alpha = 0f;
        for (float scaleUI = 0; scaleUI < 0.51f; scaleUI += 0.05f)
        {
            gameFinished.GetComponent<CanvasGroup>().alpha += 0.1f;
            gameFinished.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        gameFinished.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        yield return new WaitForSecondsRealtime(2f);

        gameFinished.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        while (gameFinished.GetComponent<CanvasGroup>().alpha > 0)
        {
            gameFinished.GetComponent<CanvasGroup>().alpha -= 0.1f;
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

