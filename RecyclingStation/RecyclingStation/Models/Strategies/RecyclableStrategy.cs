namespace RecyclingStationNS.Models.Strategies
{
    using RecyclingStationNS.Enum;
    using WasteDisposal.Interfaces;

    public class RecyclableStrategy : IGarbageDisposalStrategy
    {
        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            double garbageVolume = garbage.Weight * garbage.VolumePerKg;

            double energyBalance = -0.5 * garbageVolume;
            double capitalBalance = 400 * garbage.Weight;
            var type = GarbageType.Recyclable;

            IProcessingData data = new ProcessingData(capitalBalance, energyBalance, type);

            return data;
        }
    }
}
