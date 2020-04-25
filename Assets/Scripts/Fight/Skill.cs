namespace Fight
{
    public class Skill
    {
        public string Name { get; }
        public SkillType SkillType { get; }
        public float PositivEffect { get; }
        public float Count { get; set; }

        public bool CanUse => Count != 0;
        
        public Skill(string name, SkillType type, float positive, float count)
        {
            Name = name;
            SkillType = type;
            PositivEffect = positive;
            Count = count;
        }

        public void Used()
        {
            Count -= 1;
        }
    }
}