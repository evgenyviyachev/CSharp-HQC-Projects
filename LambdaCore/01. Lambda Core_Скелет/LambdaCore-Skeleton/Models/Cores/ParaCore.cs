namespace LambdaCore_Skeleton.Models.Cores
{
    public class ParaCore : BaseCore
    {
        public ParaCore(string type, int durability, string name)
            : base(type, durability, name)
        {
        }

        public override int Durability
        {
            get
            {
                return base.Durability;
            }
            protected set
            {
                base.Durability = value / 3;
            }
        }
    }
}
