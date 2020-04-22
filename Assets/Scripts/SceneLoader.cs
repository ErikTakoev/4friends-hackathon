using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string sceneName;
    
    void Start()
    {
        if (button == null)
        {
            LoadScene();
        }
        else
        {
            button.onClick.AddListener(LoadScene);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
