using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseObject;
    public GameObject levelObject;

    private Canvas pauseMenu;

    public static bool roundWin = false;
    public static bool roundLose = false;
    public static bool askLevels = false;
    public static bool askOptions = false;
    public static bool loadLevel = false;

    public static readonly float screenScale = (965f / Screen.width);
    private string[] LevelsName = { "MainMenu", "Prambanan1", "Prambanan2" };
    private int playerLevel = 0;

    private void Awake()
    {
        pauseObject = Instantiate(pauseObject);
        pauseMenu = pauseObject.GetComponent<Canvas>();
        pauseObject.SetActive(false);
        LoadCurrentLevel();
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(LevelsName[playerLevel], LoadSceneMode.Additive);
    }

    void Update()
    {
        if (loadLevel)
        {
            LoadCurrentLevel();
            loadLevel = false;
        }
        if(askLevels)
        {
            SceneManager.LoadScene("LevelSelection", LoadSceneMode.Additive);
            askLevels = false;
        }
        if(roundWin)
        {
            SceneManager.UnloadSceneAsync(LevelsName[playerLevel]);
            if(playerLevel == 0)
            {
                SceneManager.LoadScene("WinLoseMenu", LoadSceneMode.Additive);
            }
            else
            {
                loadLevel = true;
            }
            playerLevel++;
            roundWin = false;
        }
        if(roundLose)
        {
            roundLose = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && playerLevel != 0)
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
                pauseObject.SetActive(true);
                pauseObject.GetComponent<PauseMenu>().Pause();
                gameIsPaused = true;
            }
        }
    }



}