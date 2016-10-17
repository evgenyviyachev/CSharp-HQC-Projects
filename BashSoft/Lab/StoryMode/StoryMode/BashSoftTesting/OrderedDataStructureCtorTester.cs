namespace BashSoftTesting
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Executor.Contracts;
    using Executor.DataStructures;

    [TestClass]
    public class OrderedDataStructureCtorTester
    {
        private ISimpleOrderedBag<string> names;

        [TestMethod]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithAllParams()
        {
            this.names = new SimpleSortedList<string>((x, y) => x.CompareTo(y), 30);
            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithInitialComparison()
        {
            this.names = new SimpleSortedList<string>((x, y) => x.CompareTo(y));
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }
    }
}
