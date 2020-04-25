namespace Fight
{
    public class FighterContainer
    {
        public static Attacker Player()
        {
            var skills = new SkillList();
            
            skills.AddSkill(SkillsConstants.NormalAttack());
            skills.AddSkill(SkillsConstants.HardAttack());
            skills.AddSkill(SkillsConstants.ForkAttack());
            skills.AddSkill(SkillsConstants.Defend());
            skills.AddSkill(SkillsConstants.Cry());

            var attacker = new Attacker(150, 10, "Girl", skills);

            return attacker;
        }
        
        public static EnemyAttacker Enemy()
        {
            var skills = new SkillList();
            
            skills.AddSkill(SkillsConstants.NormalAttack());
            skills.AddSkill(SkillsConstants.HardAttack());
            skills.AddSkill(SkillsConstants.Harden());
            skills.AddSkill(SkillsConstants.Cry());
            
            var attacker = new EnemyAttacker(150, 10, "Mega CRAB", skills);

            return attacker;
        }
    }
}