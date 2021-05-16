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
        Debug.Log("here");
        GameSceneManager.loadLevel = true;
        Resume();
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

