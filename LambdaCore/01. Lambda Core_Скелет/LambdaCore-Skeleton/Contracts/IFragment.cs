namespace LambdaCore_Skeleton.Contracts
{
    using LambdaCore_Skeleton.Enums;

    public interface IFragment
    {
        string Name { get; }
        string Type { get; }
        int PressureAffection { get; }
    }
}
