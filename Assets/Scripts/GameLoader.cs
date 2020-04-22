using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader
{
    private const string SceneName = "LoadingScene";
    
    [RuntimeInitializeOnLoadMethod]
    private static void LoadGame()
    {
        ServiceLocator.Clear();

        SceneManager.LoadScene(SceneName);
    }

    public static void Reload()
    {
        LoadGame();
    }
}
