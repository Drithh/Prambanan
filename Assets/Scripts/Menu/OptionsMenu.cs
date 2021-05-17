using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    Transform gameFinished;
    public static bool optionsMenuExit = false;
    public AudioMixer audioMixer;
    Transform imageUI;

    private void Awake()
    {

        gameFinished = transform.GetChild(1).transform;
        StartCoroutine(OptionsEntrance());

        imageUI = transform.GetChild(1).GetChild(1);

    }

    private void Start()
    {
        int iteration = 1;
        foreach (var item in Screen.resolutions)
        {
            if (item.width == GameSceneManager.lastResolution.width && item.height == GameSceneManager.lastResolution.height)
            {
                imageUI.GetChild(0).GetChild(0).GetComponent<Slider>().value = (float)iteration / (float)Screen.resolutions.Length;
                imageUI.GetChild(0).GetChild(1).GetComponent<Text>().text = item.width.ToString() + "x" + item.height.ToString();
                break;
            }
            iteration++;
        }


        imageUI.GetChild(1).GetChild(0).GetComponent<Slider>().value = QualitySettings.GetQualityLevel();
        imageUI.GetChild(1).GetChild(1).GetComponent<Text>().text = QualitySettings.names[QualitySettings.GetQualityLevel()];


        float currentVolume;
        bool result;
        result = audioMixer.GetFloat("MainVolume", out currentVolume);
        if (result)
        {
            currentVolume = Mathf.Pow(10, (currentVolume / 20));
            imageUI.GetChild(2).GetChild(0).GetComponent<Slider>().value = currentVolume;
            imageUI.GetChild(2).GetChild(1).GetComponent<Text>().text = (currentVolume * 100).ToString("0") + "%";
        }
    }

    private void Update()
    {
        if (optionsMenuExit)
        {
            Exit();
            optionsMenuExit = false;
        }
    }


    public void SetResolution(float resolution)
    {
        Resolution setResolution = Screen.resolutions[(int)(resolution * Screen.resolutions.Length)];

        Screen.SetResolution(setResolution.width, setResolution.height, Screen.fullScreen);
        imageUI.GetChild(0).GetChild(1).GetComponent<Text>().text = setResolution.width.ToString() + "x" + setResolution.height.ToString();

        GameSceneManager.lastResolution = setResolution;

    }

    public void SetFullscreen()
    {
        if(Screen.fullScreen)
        {
            Screen.fullScreen = false;
        }
        else
        {
            Screen.fullScreen = true;
        }
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10 (volume) * 20);
        string.Format("{0:0.00}", 123.4567);
        imageUI.GetChild(2).GetChild(1).GetComponent<Text>().text = (volume * 100).ToString("0") + "%";
    }

    public void SetQuality (float qualityIndex)
    {
        QualitySettings.SetQualityLevel((int)qualityIndex);
        imageUI.GetChild(1).GetChild(1).GetComponent<Text>().text = QualitySettings.names[(int)qualityIndex];
    }


    public void Exit()
    {
        StartCoroutine(OptionsExit());
    }

    IEnumerator OptionsEntrance()
    {
        gameFinished.GetComponent<CanvasGroup>().alpha = 0f;
        for (float scaleUI = 0; scaleUI < 0.9f; scaleUI += 0.09f)
        {
            gameFinished.GetComponent<CanvasGroup>().alpha += 0.1f;
            gameFinished.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        gameFinished.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        yield return new WaitForSecondsRealtime(30f);
    }

    IEnumerator OptionsExit()
    {
        CanvasGroup canvasGroup = gameFinished.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        for (float scaleUI = 0.8f; scaleUI > 0f; scaleUI -= 0.08f)
        {
            canvasGroup.alpha -= 0.1f;
            gameFinished.transform.localScale = new Vector3(scaleUI, scaleUI, scaleUI);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        SceneManager.UnloadSceneAsync("OptionsMenu");
    }
}
