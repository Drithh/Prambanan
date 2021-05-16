using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseMenu : PauseMenu
{
    public void LoadLevel()
    {
        GameManager.loadLevel = true;
    }
}

