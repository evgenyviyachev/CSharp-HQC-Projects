namespace RecyclingStationTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RecyclingStationNS.Enum;
    using RecyclingStationNS.WasteDisposal;
    using RecyclingStationNS.WasteDisposal.Interfaces;
    using Setup;
    
    [TestClass]
    public class GarbageProcessorTests
    {
        private IGarbageProcessor gp;

        [TestInitialize]
        public void TestInit()
        {
            this.gp = new GarbageProcessor();
            var type = typeof(FakeAttribute);
            var strategy = new FakeStrategy();
            this.gp.StrategyHolder.AddStrategy(type, strategy);
        }

        [TestMethod]
        public void ProcessWasteShouldWorkCorrectly()
        {
            var fakeWaste = new FakeWaste();
            var result = this.gp.ProcessWaste(fakeWaste);

            Assert.AreEqual(100, result.CapitalBalance);
            Assert.AreEqual(200, result.EnergyBalance);
            Assert.AreEqual(GarbageType.Burnable, result.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProcessWasteWithoutDisposableAttributeShouldThrow()
        {
            var fakeWaste = new FakeWasteWithoutAttribute();
            this.gp.ProcessWaste(fakeWaste);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProcessWasteWithAttributeNotInHolderShouldThrow()
        {
            var fakeWaste = new FakeWasteWithAttributeNotInHolder();
            this.gp.ProcessWaste(fakeWaste);
        }
    }
}
