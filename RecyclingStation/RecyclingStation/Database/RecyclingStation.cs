namespace RecyclingStationNS.Database
{
    using System;
    using Contracts;
    using WasteDisposal.Interfaces;
    using Enum;

    public class RecyclingStation : IRecyclingStation
    {
        private double capital;
        private double energy;
        private GarbageType typeOfGarbageToCheck;
        private double energyBalanceMin;
        private double capitalBalanceMin;
        private bool isManagementSet;
                
        public RecyclingStation()
        {
            this.capital = 0;
            this.energy = 0;
            this.energyBalanceMin = 0;
            this.capitalBalanceMin = 0;
            this.isManagementSet = false;
        }

        public string ChangeGarbageToCheck(string type, double energyBalanceMin, double capitalBalanceMin)
        {
            if (!this.isManagementSet)
            {
                this.isManagementSet = true;
            }

            this.typeOfGarbageToCheck = (GarbageType)Enum.Parse(typeof(GarbageType), type);
            this.energyBalanceMin = energyBalanceMin;
            this.capitalBalanceMin = capitalBalanceMin;

            return "Management requirement changed!";
        }

        public bool UpdateData(IProcessingData data)
        {
            if (this.isManagementSet &&
                data.Type == this.typeOfGarbageToCheck
                && (this.capitalBalanceMin > this.capital
                || this.energyBalanceMin > this.energy))
            {
                return false;
            }

            this.energy += data.EnergyBalance;
            this.capital += data.CapitalBalance;

            return true;
        }

        public override string ToString()
        {
            return $"Energy: {this.energy:F2} Capital: {this.capital:F2}";
        }
    }
}
