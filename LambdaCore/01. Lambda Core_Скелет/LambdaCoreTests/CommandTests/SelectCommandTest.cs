namespace LambdaCoreTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LambdaCore_Skeleton.Contracts;
    using LambdaCore_Skeleton.DatabaseNS;

    [TestClass]
    public class SelectCommandTest
    {
        private IDatabase db;

        [TestInitialize]
        public void TestInit()
        {
            this.db = new Database();
        }

        [TestMethod]
        public void SelectShouldReturnFailWhenDBIsEmpty()
        {
            string result = this.db.SelectCore(new[] { "A" });
            Assert.IsTrue(result == "Failed to select Core A!");
        }

        [TestMethod]
        public void SelectShouldReturnSuccess()
        {
            this.db.CreateCore(new[] { "Para", "100" });
            string result = this.db.SelectCore(new[] { "A" });
            Assert.IsTrue(result == "Currently selected Core A!");
        }
    }
}
