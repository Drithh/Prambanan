using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseObject;
    public GameObject levelObject;

    public static readonly float screenScale = (965f / Screen.width);

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                if (SceneManager.GetSceneByName("LevelSelection").isLoaded){
                    levelObject.GetComponent<LevelSelection>().Exit();
                }
                else
                {
                    pauseObject.GetComponent<PauseMenu>().Resume();
                    gameIsPaused = false;
                }
            }
            else
            {
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                gameIsPaused = true;
            }
        }
    }



}