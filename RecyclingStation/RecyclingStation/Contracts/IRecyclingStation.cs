namespace RecyclingStationNS.Contracts
{
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public interface IRecyclingStation
    {
        bool UpdateData(IProcessingData data);
        string ChangeGarbageToCheck(string type, double energyBalanceMin, double capitalBalanceMin);
    }
}
