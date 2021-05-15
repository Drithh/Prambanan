using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public GameObject levelMenuUI;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        StartCoroutine(EntranceUI());
    }

    public void Exit()
    {
        SceneManager.UnloadSceneAsync("LevelSelection");
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
}

