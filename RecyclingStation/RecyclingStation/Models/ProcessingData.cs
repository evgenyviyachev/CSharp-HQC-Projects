namespace RecyclingStationNS.Models
{
    using RecyclingStationNS.Enum;
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public class ProcessingData : IProcessingData
    {
        public ProcessingData(double capitalBalance, double energyBalance, GarbageType type)
        {
            this.CapitalBalance = capitalBalance;
            this.EnergyBalance = energyBalance;
            this.Type = type;
        }

        public double CapitalBalance { get; private set; }

        public double EnergyBalance { get; private set; }

        public GarbageType Type { get; set; }
    }
}
