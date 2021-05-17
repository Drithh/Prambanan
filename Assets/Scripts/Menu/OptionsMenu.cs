using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(OptionsMessage());
    }

    IEnumerator OptionsMessage()
    {
        Transform gameFinished = transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().transform;
        gameFinished.GetComponent<CanvasGroup>().alpha = 0f;
        for (float scaleUI = 0; scaleUI < 0.51f; scaleUI += 0.05f)
        {
            gameFinished.GetComponent<CanvasGroup>().alpha += 0.1f;
            gameFinished.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        gameFinished.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        yield return new WaitForSecondsRealtime(4f);

        while (gameFinished.GetComponent<CanvasGroup>().alpha > 0)
        {
            gameFinished.GetComponent<CanvasGroup>().alpha -= 0.1f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        SceneManager.UnloadSceneAsync("OptionsMenu");
    }
}
