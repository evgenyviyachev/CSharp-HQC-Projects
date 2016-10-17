namespace LambdaCoreTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LambdaCore_Skeleton.Contracts;
    using LambdaCore_Skeleton.DatabaseNS;

    [TestClass]
    public class DetachFragmentTest
    {
        private IDatabase db;

        [TestInitialize]
        public void TestInit()
        {
            this.db = new Database();
        }

        [TestMethod]
        public void WhenNoAttachedFragments_ReturnsFail()
        {
            //TO-DO
        }

        [TestMethod]
        public void NoSelectedCoreShouldReturnFail()
        {
            //TO-DO
        }

        [TestMethod]
        public void CommandReturnsSuccess()
        {
            //TO-DO
        }
    }
}
