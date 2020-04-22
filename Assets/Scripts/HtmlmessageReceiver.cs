using System;
using UnityEngine;

public class HtmlmessageReceiver : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void RestartGame()
    {
        GameLoader.Reload();        
    }
}