using System;
using Boo.Lang;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fight
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text skillText;
        [SerializeField] private TMP_Text buttonText;

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
        }

        private void UpdateView()
        {
            buttonText.text = $"{skill.Name}";
            skillText.text = $"{skill.SkillType} ({skill.Count}) \n {skill.PositivEffect}";
        }
    }
}