using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader
{
    private const string SceneName = "LoadingScene";
    
    [RuntimeInitializeOnLoadMethod]
    private static void LoadGame()
    {
#if UNITY_EDITOR
        
        if (SceneManager.GetActiveScene().name.ToLower().Contains("test"))
        {
            return;
        }

#endif

        ServiceLocator.Clear();

        SceneManager.LoadScene(SceneName);
    }

    public static void Reload()
    {
        LoadGame();
    }
}
