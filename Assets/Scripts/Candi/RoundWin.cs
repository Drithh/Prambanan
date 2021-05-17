using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundWin : MonoBehaviour
{
    public int difficultyLevel;
    private bool askedToWin = false;

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("GuideParent") == null && !askedToWin && !GameObject.Find("Tutorial"))
        {
            askedToWin = true;
            GameSceneManager.RoundWin();
        }
    }


}
