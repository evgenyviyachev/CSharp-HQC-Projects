namespace RecyclingStationNS.Models.Waste
{
    using RecyclingStationNS.WasteDisposal.Attributes;

    [Recyclable]
    public class RecyclableGarbage : Waste
    {
        public RecyclableGarbage(string name, double weight, double volumePerKg)
            : base(name, weight, volumePerKg)
        {
        }
    }
}
