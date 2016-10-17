namespace RecyclingStationNS.Models.Waste
{
    using RecyclingStationNS.WasteDisposal.Attributes;

    [Storable]
    public class StorableGarbage : Waste
    {
        public StorableGarbage(string name, double weight, double volumePerKg) 
            : base(name, weight, volumePerKg)
        {
        }
    }
}
