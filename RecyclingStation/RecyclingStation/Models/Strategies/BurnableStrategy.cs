namespace RecyclingStationNS.Models.Strategies
{
    using RecyclingStationNS.Enum;
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public class BurnableStrategy : IGarbageDisposalStrategy
    {
        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            double garbageVolume = garbage.Weight * garbage.VolumePerKg;

            double energyBalance = (1 - 0.2) * garbageVolume;
            double capitalBalance = 0;
            var type = GarbageType.Burnable;

            IProcessingData data = new ProcessingData(capitalBalance, energyBalance, type);

            return data;
        }
    }
}
