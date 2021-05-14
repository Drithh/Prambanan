using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                SceneManager.UnloadSceneAsync("PauseMenu");
                gameIsPaused = false;
            }
            else
            {
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                gameIsPaused = true;
            }
        }
    }



}