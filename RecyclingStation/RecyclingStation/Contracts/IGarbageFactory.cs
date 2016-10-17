namespace RecyclingStationNS.Contracts
{
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public interface IGarbageFactory
    {
        IWaste CreateGarbage(string type, string name, double weight, double volumePerKg);
    }
}
