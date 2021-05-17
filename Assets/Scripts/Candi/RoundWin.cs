using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundWin : MonoBehaviour
{
    public float difficultyLevel;
    private bool askedToWin = false;

    private void Update()
    {
        if (((GameObject.FindGameObjectWithTag("GuideParent") == null  && !GameObject.Find("Tutorial")) || Input.GetKeyDown(KeyCode.F12)) && !askedToWin)
        {
            askedToWin = true;
            GameSceneManager.RoundWin();

        }
    }
}
