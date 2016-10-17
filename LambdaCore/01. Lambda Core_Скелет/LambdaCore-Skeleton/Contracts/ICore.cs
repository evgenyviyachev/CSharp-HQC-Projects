namespace LambdaCore_Skeleton.Contracts
{
    using Collection;
    using LambdaCore_Skeleton.Enums;

    public interface ICore
    {
        string Type { get; }
        int Durability { get; }
        string Name { get; }
        LStack Fragments { get; }
        CoreState State { get; }
        int PressureOnCore { get; }
        string AttachFragment(IFragment fragment);
        string DetachFragment();
    }
}
