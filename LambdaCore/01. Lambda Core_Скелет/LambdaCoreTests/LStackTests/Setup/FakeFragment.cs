namespace LambdaCoreTests
{
    using LambdaCore_Skeleton.Contracts;
    using LambdaCore_Skeleton.Enums;

    public class FakeFragment : IFragment
    {
        public string Name
        {
            get
            {
                return "A36";
            }
        }

        public int PressureAffection
        {
            get
            {
                return 100;
            }
        }

        public string Type
        {
            get
            {
                return FragmentType.Cooling.ToString();
            }
        }
    }
}
