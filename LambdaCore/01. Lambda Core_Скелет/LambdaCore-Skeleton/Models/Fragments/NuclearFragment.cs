namespace LambdaCore_Skeleton.Models.Fragments
{
    public class NuclearFragment : BaseFragment
    {
        public NuclearFragment(string type, string name, int pressureAffection)
            : base(type, name, pressureAffection)
        {
        }

        public override int PressureAffection
        {
            get
            {
                return base.PressureAffection;
            }
            protected set
            {
                base.PressureAffection = value * 2;
            }
        }
    }
}
