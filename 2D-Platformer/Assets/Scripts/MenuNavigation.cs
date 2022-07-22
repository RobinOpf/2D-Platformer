using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void GoToLevelX(int level)
    {
        SceneManager.LoadScene("Level " + level);
    }

    public void GoToDebugWorld()
    {
        SceneManager.LoadScene("DebugWorld");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

