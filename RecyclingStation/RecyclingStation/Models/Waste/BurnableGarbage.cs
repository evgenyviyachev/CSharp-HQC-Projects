namespace RecyclingStationNS.Models.Waste
{
    using RecyclingStationNS.WasteDisposal.Attributes;

    [Burnable]
    public class BurnableGarbage : Waste
    {
        public BurnableGarbage(string name, double weight, double volumePerKg)
            : base(name, weight, volumePerKg)
        {
        }
    }
}
