using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Pause();
    }

    private void Pause()
    {
        StartCoroutine(EntranceUI());
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("PauseMenu");
        GameManager.gameIsPaused = false;
    }

    public void Levels()
    {
        SceneManager.LoadScene("LevelSelection", LoadSceneMode.Additive);
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator EntranceUI()
    {
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
}

