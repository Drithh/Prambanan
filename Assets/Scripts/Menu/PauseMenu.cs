using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private CanvasGroup canvasGroup;

    public void Pause()
    {
        StartCoroutine(EntranceUI());
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        StartCoroutine(ExitUi());
        Time.timeScale = 1f;
    }

    public void Levels()
    {
        GameManager.askLevels = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator EntranceUI()
    {
        gameObject.SetActive(true);
        canvasGroup = pauseMenuUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        for (int locationUI = -570; locationUI < 20 ; locationUI += 10)
        {
            if(locationUI % 50 == 0)
            {
                canvasGroup.alpha += 0.1f;
            }
            pauseMenuUI.transform.localPosition = new Vector3(0, locationUI, 0);
            yield return new WaitForSecondsRealtime(0.008f);
        }
        pauseMenuUI.transform.localPosition = new Vector3(0, 0, 0);
    }

    IEnumerator ExitUi()
    {
        while (pauseMenuUI.GetComponent<CanvasGroup>().alpha > 0)
        {
            pauseMenuUI.GetComponent<CanvasGroup>().alpha -= 0.1f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        gameObject.SetActive(false);

    }
}

