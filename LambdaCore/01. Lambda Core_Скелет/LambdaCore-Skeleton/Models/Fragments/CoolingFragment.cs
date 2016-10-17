namespace LambdaCore_Skeleton.Models.Fragments
{
    public class CoolingFragment : BaseFragment
    {
        public CoolingFragment(string type, string name, int pressureAffection)
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
                base.PressureAffection = value * 3;
            }
        }
    }
}
