namespace RecyclingStationTests
{
    using Setup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RecyclingStationNS.WasteDisposal.Interfaces;
    using RecyclingStationNS.WasteDisposal;
    
    [TestClass]
    public class StrategyHolderTests
    {
        private IStrategyHolder sh;

        [TestInitialize]
        public void TestInit()
        {
            this.sh = new StrategyHolder();
        }

        [TestMethod]
        public void AddStrategyShouldWorkCorrectly()
        {
            var type = typeof(FakeAttribute);
            var strategy = new FakeStrategy();

            this.sh.AddStrategy(type, strategy);

            Assert.IsTrue(this.sh.GetDisposalStrategies.ContainsKey(type));
        }

        [TestMethod]
        public void AddStrategyShouldReturnTrueWhenNewAttributeIsPassed()
        {
            var type = typeof(FakeAttribute);
            var strategy = new FakeStrategy();
            
            Assert.IsTrue(this.sh.AddStrategy(type, strategy));
        }

        [TestMethod]
        public void AddStrategyShouldReturnFalseWhenExistingAttributeIsPassed()
        {
            var type = typeof(FakeAttribute);
            var strategy = new FakeStrategy();

            this.sh.AddStrategy(type, strategy);

            Assert.IsFalse(this.sh.AddStrategy(type, strategy));
        }

        [TestMethod]
        public void RemoveStrategyShouldWorkCorrectly()
        {
            var type = typeof(FakeAttribute);
            var strategy = new FakeStrategy();

            this.sh.AddStrategy(type, strategy);
            this.sh.RemoveStrategy(type);

            Assert.IsFalse(this.sh.GetDisposalStrategies.ContainsKey(type));
        }

        [TestMethod]
        public void RemoveStrategyShouldReturnTrueWhenExistingAttributeIsPassed()
        {
            var type = typeof(FakeAttribute);
            var strategy = new FakeStrategy();

            this.sh.AddStrategy(type, strategy);

            Assert.IsTrue(this.sh.RemoveStrategy(type));
        }

        [TestMethod]
        public void RemoveStrategyShouldReturnFalseWhenNonExistingAttributeIsPassed()
        {
            var type = typeof(FakeAttribute);

            Assert.IsFalse(this.sh.RemoveStrategy(type));
        }
    }
}
