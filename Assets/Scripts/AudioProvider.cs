using System;
using UnityEngine;

public class AudioProvider : MonoBehaviour
{
    [SerializeField] private AudioClip kick;
    [SerializeField] private AudioSource audioSource;
    
    private void Awake()
    {
        ServiceLocator.Add(this);
    }

    public void PlayOnShotKick()
    {
        audioSource.PlayOneShot(kick);   
    }
}