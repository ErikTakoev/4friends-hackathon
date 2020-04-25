namespace Fight
{
    public class SkillsConstants
    {
        public static Skill NormalAttack()
        {
            var skill = new Skill("Attack", SkillType.Attack, 15, 100);

            return skill;
        }
        
        public static Skill Defend()
        {
            var skill = new Skill("Defend", SkillType.Defend, 10, 100);

            return skill;
        }
        
        public static Skill HardAttack()
        {
            var skill = new Skill("Hard Attack", SkillType.Attack, 25, 5);

            return skill;
        }
        
        public static Skill ForkAttack()
        {
            var skill = new Skill("Use Fork", SkillType.Attack, 30, 3);

            return skill;
        }
        
        public static Skill Cry()
        {
            var skill = new Skill("Cry", SkillType.Defend, 35, 3);

            return skill;
        }
        
        public static Skill Harden ()
        {
            var skill = new Skill("Harden", SkillType.Defend, 25, 3);

            return skill;
        }
    }
}