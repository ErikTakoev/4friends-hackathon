using System.Collections.Generic;

namespace Fight
{
    public class SkillList
    {
        public List<Skill> Skills = new List<Skill>();

        public void AddSkill(Skill skill)
        {
            Skills.Add(skill);
        }
    }
}