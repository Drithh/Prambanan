using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime = 600f;
    private float startingTime = 0f;
    private float milliseconds, seconds, minutes;
    
    private bool askedToLose = false;
    private bool askedRedAlert = false;

    private void Start()
    {
        StartCoroutine(GetTotalTime());
    }
    
    private void Update()
    {
        if(startingTime != 0)
        {
            currentTime -= Time.deltaTime;
            milliseconds = (currentTime % 1) * 100;
            seconds = ((int) currentTime % 60);
            minutes = (int) (currentTime / 60);
            if(minutes < 1 && seconds < 15 && !askedRedAlert)
            {
                askedRedAlert = true;
                StartCoroutine(TimerAlert());
            }
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = minutes.ToString("0").PadLeft(2, '0') + ":" + seconds.ToString("0").PadLeft(2, '0')  + ":" + milliseconds.ToString("0");

            if(currentTime < 0 && !askedToLose)
            {
                askedToLose = true;
                WinLoseMenu.loseTo = 1;
                GameSceneManager.RoundLose();
            }
        }

    }

    IEnumerator GetTotalTime()
    {
        yield return new WaitForSeconds(3f);
        if(CandiSpawner.totalSpawnAllBlock > 0)
        {
            startingTime = ((CandiSpawner.totalSpawnAllBlock / GameObject.Find("Game").GetComponent<RoundWin>().difficultyLevel) * 15);
            currentTime = startingTime;
        }
        else
        {
            startingTime = currentTime;
        }
    }

    IEnumerator TimerAlert()
    {
        while(!askedToLose)
        {
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = Color.red;
            yield return new WaitForSeconds(1f);
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = Color.white;
            yield return new WaitForSeconds(1f);
        }
    }
}
