namespace RecyclingStationNS.Models.Waste
{
    using WasteDisposal.Interfaces;

    public abstract class Waste : IWaste
    {
        private string name;
        private double weight;
        private double volumePerKg;

        public Waste(string name, double weight, double volumePerKg)
        {
            this.name = name;
            this.weight = weight;
            this.volumePerKg = volumePerKg;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public double VolumePerKg
        {
            get
            {
                return this.volumePerKg;
            }
        }

        public double Weight
        {
            get
            {
                return this.weight;
            }
        }
    }
}
