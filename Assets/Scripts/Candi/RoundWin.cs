using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundWin : MonoBehaviour
{
    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("GuideParent") == null)
        {
            GameManager.roundWin = true;
        }
    }
}
