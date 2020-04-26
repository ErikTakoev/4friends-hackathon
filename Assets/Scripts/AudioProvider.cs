using System;
using UnityEngine;

public class AudioProvider : MonoBehaviour
{
    [SerializeField] private AudioClip kick;
    [SerializeField] private AudioClip fightClip;
    [SerializeField] private AudioClip backMusic;
    [SerializeField] private AudioSource audioSource;
    
    private void Awake()
    {
        ServiceLocator.Add(this);

        PlayNormal();
    }

    public void PlayOnShotKick()
    {
        audioSource.PlayOneShot(kick);   
    }

    public void PlayOnShotClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void PlayFightClip()
    {
        audioSource.Stop();

        audioSource.clip = fightClip;
        
        audioSource.Play();
    }

    public void PlayNormal()
    {
        audioSource.Stop();

        audioSource.clip = backMusic;
        
        audioSource.Play();
    }
}