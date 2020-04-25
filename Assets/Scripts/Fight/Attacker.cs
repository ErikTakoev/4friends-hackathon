using UnityEngine;

namespace Fight
{
    public class Attacker
    {
        public float Hp { get; }
        public int Level { get; }
        public string Name { get; }
        public SkillList SkillList { get; }

        public float CurrentHp;
        
        public Attacker(float hp, int level, string name, SkillList skils)
        {
            Hp = hp;
            Level = level;
            Name = name;
            SkillList = skils;

            CurrentHp = Hp;
        }

        public void ApplyHp(Skill skill)
        {
            if (skill.SkillType == SkillType.Attack)
            {
                CurrentHp -= skill.PositivEffect;
            }
            
            if (skill.SkillType == SkillType.Defend)
            {
                CurrentHp += skill.PositivEffect;
            }

            CurrentHp = Mathf.Clamp(CurrentHp, 0, Hp);
        }
    }
}