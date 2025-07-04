using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text skillText;
        [SerializeField] private TMP_Text buttonText;
        [SerializeField] private Image icon;
        
        private Button button;
        private Skill skill;
        
        public Action<Skill> onUse;
        
        private void Awake()
        {
            button = GetComponentInChildren<Button>();
            
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            onUse?.Invoke(skill);
            
            UpdateView();
        }

        public void Init(Skill skill)
        {
            this.skill = skill;

            UpdateView();
            icon.sprite = this.skill.Icon;
        }

        private void UpdateView()
        {
            if (!skill.CanUse)
            {
                button.enabled = false;
            }

            buttonText.text = $"{skill.Name}";
            skillText.text = $"{skill.SkillType} ({skill.Count}) \n {skill.PositivEffect}";
        }
    }
}