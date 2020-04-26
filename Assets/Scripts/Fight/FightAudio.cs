using System;
using UnityEngine;

namespace Fight
{
    public class FightAudio : MonoBehaviour
    {
        private AudioProvider audioProvider => ServiceLocator.Get<AudioProvider>();

        private void Awake()
        {
            if (audioProvider)
            {
                audioProvider.PlayFightClip();
            }
        }

        public void PlayHeal()
        {
            
        }

        public void PlayHit()
        {
            
        }

        public void PlayAmbient()
        {
            
        }

        public void ReturnToNormal()
        {
            if (audioProvider)
            {
                audioProvider.PlayNormal();
            }
        }
    }
}