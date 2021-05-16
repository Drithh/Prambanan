using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundWin : MonoBehaviour
{
    private bool askedToWin = false;

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("GuideParent") == null && !askedToWin)
        {
            askedToWin = true;
            GameSceneManager.RoundWin();
        }
    }


}
