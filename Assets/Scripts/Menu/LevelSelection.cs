using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public GameObject levelMenuUI;


    private CanvasGroup canvasGroup;

    private void Awake()
    {
        StartCoroutine(EntranceUI());
        for (int i = 0; i < GameSceneManager.playerLevel; i++)
        {
            transform.GetChild(0).GetChild(1).GetChild(i + 2).GetComponent<Button>().interactable = true;
        }
    }

    public void Loadlevel(int buttonlevel = 0)
    {
        if (buttonlevel > 0)
        {
            GameSceneManager.askedLevel = buttonlevel + 4;
        }
        GameSceneManager.loadLevel = true;

        GameSceneManager.askAllUIClose = true;
        StartCoroutine(ExitUi());
    }


    public void Exit()
    {
        StartCoroutine(ExitUi());
    }

    IEnumerator EntranceUI()
    {
        canvasGroup = levelMenuUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        for (float scaleUI = 0; scaleUI < 0.71f; scaleUI += 0.07f)
        {
            canvasGroup.alpha += 0.1f;
            levelMenuUI.transform.localScale = new Vector3 (scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        levelMenuUI.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    IEnumerator ExitUi()
    {
        levelMenuUI.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        canvasGroup = levelMenuUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        for (float scaleUI = 0.8f; scaleUI > 0f; scaleUI -= 0.08f)
        {
            canvasGroup.alpha -= 0.1f;
            levelMenuUI.transform.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        SceneManager.UnloadSceneAsync("LevelSelection");
    }
}

