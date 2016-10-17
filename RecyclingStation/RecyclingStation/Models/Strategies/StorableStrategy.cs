namespace RecyclingStationNS.Models.Strategies
{
    using RecyclingStationNS.Enum;
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public class StorableStrategy : IGarbageDisposalStrategy
    {
        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            double garbageVolume = garbage.Weight * garbage.VolumePerKg;

            double energyBalance = -0.13 * garbageVolume;
            double capitalBalance = -0.65 * garbageVolume;
            var type = GarbageType.Storable;

            IProcessingData data = new ProcessingData(capitalBalance, energyBalance, type);

            return data;
        }
    }
}
