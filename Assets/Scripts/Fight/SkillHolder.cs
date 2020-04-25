using System;
using UnityEngine;

namespace Fight
{
    public class SkillHolder : MonoBehaviour, IElement
    {
        [SerializeField] private SkillView skillPrefab;
        [SerializeField] private Transform parentTransform;
        
        private SkillList skillList;

        public Action<Skill> OnUseSkill;
        
        public void Init(Attacker skills)
        {
            skillList = skills.SkillList;
            
            CreateSkillView();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void CreateSkillView()
        {

            foreach (var skill in skillList.Skills)
            {
                var view = Instantiate(skillPrefab, parentTransform);

                view.Init(skill);
                
                view.onUse += OnSkillUsed;
            }
        }

        private void OnSkillUsed(Skill skill)
        {
            OnUseSkill?.Invoke(skill);
        }
    }
}