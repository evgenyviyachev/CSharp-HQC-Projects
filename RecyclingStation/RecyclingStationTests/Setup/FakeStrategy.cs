namespace RecyclingStationTests.Setup
{
    using RecyclingStationNS.WasteDisposal.Interfaces;

    public class FakeStrategy : IGarbageDisposalStrategy
    {
        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            return new FakeProcessingData();
        }
    }
}
