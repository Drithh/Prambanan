using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public GameObject pauseObject;
    public GameObject levelObject;
    private Canvas pauseMenu;

    public static readonly float screenScale = (965f / Screen.width);

    public static bool gameIsPaused     = false;
    public static bool askLevels        = false;
    public static bool askOptions       = false;
    public static bool loadLevel        = false;
    public static bool nextLevel        = false;
    public static bool askAllUIClose    = false;

    public static int playerLevel = 0;
    public static int askedLevel;

    private void Awake()
    {

        pauseObject = Instantiate(pauseObject);
        pauseMenu = pauseObject.GetComponent<Canvas>();
        pauseObject.SetActive(false);

        // Load Menu
        StartCoroutine(SceneLoader(1));
    }

    void Update()
    {
        if(askOptions)
        {
            SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Additive);
            askOptions = false;
        }
        if(askLevels)
        {
            SceneManager.LoadScene("LevelSelection", LoadSceneMode.Additive);
            askLevels = false;
        }

        if (nextLevel)
        {
            if (playerLevel == 0) playerLevel++;
            LoadLevel(playerLevel + 4);
            nextLevel = false;
        }

        if (loadLevel)
        {
            LoadLevel();
            loadLevel = false;
        }

        if(askAllUIClose)
        {
            closePauseMenu();
            askAllUIClose = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && playerLevel != 0 && (!SceneManager.GetSceneByName("WinLoseMenu").isLoaded))
        {
            PauseMenu();
        }
    }

    public void LoadLevel(int sceneIndex = -1)
    {
        if (sceneIndex == -1)
        {
            if(askedLevel > 0)
            {
                sceneIndex = askedLevel;
                askedLevel = 0;
            }
            else
            {
                sceneIndex = SceneManager.GetActiveScene().buildIndex;
            }
        }
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        StartCoroutine(SceneLoader(sceneIndex));
    }

    private void closePauseMenu()
    {
        if (SceneManager.GetSceneByName("WinLoseMenu").isLoaded)
        {
            WinLoseMenu.askExit = true;
        }
        if (gameIsPaused)
        {
            pauseObject.GetComponent<PauseMenu>().Resume();
            gameIsPaused = false;
        }
    }

    private void PauseMenu()
    {
        if (gameIsPaused)
        {
            if (SceneManager.GetSceneByName("LevelSelection").isLoaded)
            {
                levelObject.GetComponent<LevelSelection>().Exit();
            }
            else
            {
                if (gameIsPaused)
                {
                    pauseObject.GetComponent<PauseMenu>().Resume();
                    gameIsPaused = false;
                }
            }
        }
        else
        {
            pauseObject.SetActive(true);
            pauseObject.GetComponent<PauseMenu>().Pause();
            gameIsPaused = true;
        }
    }

    public static void RoundWin()
    {
        SceneManager.LoadScene("WinLoseMenu", LoadSceneMode.Additive);
        playerLevel++;
        if (playerLevel == 6) playerLevel--;
        WinLoseMenu.condition = 1;
    }

    public static void RoundLose()
    {
        SceneManager.LoadSceneAsync("WinLoseMenu", LoadSceneMode.Additive);
        WinLoseMenu.condition = 0;
    }


    IEnumerator SceneLoader(int sceneIndex)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        yield return new WaitUntil(() => async.isDone);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }
}