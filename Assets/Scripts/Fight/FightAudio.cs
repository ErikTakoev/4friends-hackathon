using System;
using UnityEngine;

namespace Fight
{
    public class FightAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip bossAttack;
        [SerializeField] private AudioClip girlAttack;
        [SerializeField] private AudioClip girlKick;
        [SerializeField] private AudioClip bossKick;
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

        public void BossAttack()
        {
            if (audioProvider)
            {
                audioProvider.PlayOnShotClip(bossAttack);
            }
        }

        public void GirlAttack()
        {
            if (audioProvider)
            {
                audioProvider.PlayOnShotClip(girlAttack);
            }
        }

        public void BossKick()
        {
            if (audioProvider)
            {
                audioProvider.PlayOnShotClip(bossKick);
            }
        }
        
        public void GirlKick()
        {
            if (audioProvider)
            {
                audioProvider.PlayOnShotClip(girlKick);
            }
        }
    }
}