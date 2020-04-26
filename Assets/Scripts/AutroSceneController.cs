using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AutroSceneController : MonoBehaviour
{
    [SerializeField] private VideoPlayer player;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private Image firstFrame;

    private void Start()
    {
        firstFrame.gameObject.SetActive(false);

        player.gameObject.SetActive(true);
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Video", "Autro.mp4");
        player.prepareCompleted += OnPrepared;
        player.loopPointReached += OnVideoEnd;
        player.Prepare();
    }
    
    private void OnPrepared(VideoPlayer player)
    {
        player.Play();
        rawImage.texture = player.texture;
    }
    
    private void OnVideoEnd(VideoPlayer player)
    {
        Debug.Log("Intro finished.");
        
        firstFrame.gameObject.SetActive(true);
    }
}
