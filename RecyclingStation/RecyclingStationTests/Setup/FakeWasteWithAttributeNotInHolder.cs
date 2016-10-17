namespace RecyclingStationTests.Setup
{
    using RecyclingStationNS.WasteDisposal.Interfaces;

    [Fake2]
    public class FakeWasteWithAttributeNotInHolder : IWaste
    {
        private string name = "Trash";
        private double volumePerKg = 100;
        private double weight = 5;

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
