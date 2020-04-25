using UnityEngine;

namespace Fight
{
    public class EnemyAttacker : Attacker
    {
        public EnemyAttacker(float hp, int level, string name, SkillList skils) : base(hp, level, name, skils)
        {
            
        }

        public Skill ChoseMove()
        {
            if (CurrentHp >= Hp / 2)
            {
                var skill = FindSkill("Hard Attack");

                if (skill.CanUse)
                {
                    return skill;
                }

            }

            if (CurrentHp <= Hp / 2 && Random.Range(0,5) == 2)
            {
                var skill = FindSkill("Harden");

                if (skill.CanUse)
                {
                    return skill;
                }
            }

            if (Random.Range(0, 10) == 5)
            {
                var skill = FindSkill("Cry");

                if (skill.CanUse)
                {
                    return skill;
                }
            }

            var randomSkill = SkillList.Skills.Find(f => f.CanUse);

            return randomSkill;
        }


        private Skill FindSkill(string name)
        {
            return SkillList.Skills.Find(f => f.Name == name);
        }
    }
}