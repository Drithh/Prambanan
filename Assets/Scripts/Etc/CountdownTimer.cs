using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime = 100f;
    float milliseconds, seconds, minutes;

    private void Start()
    {
        currentTime = startingTime;
    }
    private void Update()
    {
        currentTime -= Time.deltaTime;
        milliseconds = (currentTime % 1) * 100;
        seconds = ((int) currentTime % 60);
        minutes = (int) (currentTime / 60);
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = minutes.ToString("0").PadLeft(2, '0') + ":" + seconds.ToString("0").PadLeft(2, '0')  + ":" + milliseconds.ToString("0");
    }
}
