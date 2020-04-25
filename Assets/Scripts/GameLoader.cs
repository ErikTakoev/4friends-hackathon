using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader
{
    private const string SceneName = "LoadingScene";
    private const string InstanceScene = "InstanceLoader";
    
    [RuntimeInitializeOnLoadMethod]
    private static void LoadGame()
    {
        ServiceLocator.Clear();
        
        SceneManager.LoadScene(InstanceScene, LoadSceneMode.Additive);
        
#if UNITY_EDITOR
        
        if (SceneManager.GetActiveScene().name.ToLower().Contains("test"))
        {
            return;
        }

#endif


        SceneManager.LoadScene(SceneName);
    }

    public static void Reload()
    {
        LoadGame();
    }
}
