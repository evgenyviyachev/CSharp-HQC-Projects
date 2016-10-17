namespace RecyclingStationTests.Setup
{
    using RecyclingStationNS.Enum;
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public class FakeProcessingData : IProcessingData
    {
        private double capitalBalance = 100;
        private double energyBalance = 200;
        private GarbageType type = GarbageType.Burnable;

        public double CapitalBalance
        {
            get
            {
                return this.capitalBalance;
            }
        }

        public double EnergyBalance
        {
            get
            {
                return this.energyBalance;
            }
        }

        public GarbageType Type
        {
            get
            {
                return this.type;
            }
        }
    }
}
