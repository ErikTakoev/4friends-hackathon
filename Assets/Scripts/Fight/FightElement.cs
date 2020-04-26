using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
    [System.Serializable]
    public class FightElement : IElement
    {
        [SerializeField] private Image image;
        [SerializeField] private HpBarViewController hpBarView;
        [SerializeField] private TMP_Text name;
        [SerializeField] private Animation animation;
        [SerializeField] private Animator animator;
        
        public void Show()
        {
            image.gameObject.SetActive(true);   
            hpBarView.gameObject.SetActive(true);
        }

        public void Hide()
        {
            image.gameObject.SetActive(false);
            hpBarView.gameObject.SetActive(false);
        }

        public void UpdateView(Attacker attacker)
        {
            name.text = attacker.Name;
            
            hpBarView.UpdateView(attacker);
        }

        public void PlayAnim(SkillType type)
        {
            if (type == SkillType.Attack)
            {
                animation.Play("hit");
            }
            else
            {
                animation.Play("heal");
            }
        }

        public void AnimatorTransition()
        {
            animator.SetTrigger("Do");
        }

        public void AttackAnim()
        {
            animation.Play("GirlAttackBoss");
        }
    }
}