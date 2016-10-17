namespace LambdaCoreTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using LambdaCore_Skeleton.Contracts;
    using LambdaCore_Skeleton.DatabaseNS;

    [TestClass]
    public class AttachFragmentTest
    {
        private IDatabase db;

        [TestInitialize]
        public void TestInit()
        {
            this.db = new Database();
        }

        [TestMethod]
        public void DifferentTypeShouldReturnFail()
        {
            //TO-DO
        }

        [TestMethod]
        public void NegativePressureAffectionShouldReturnFail()
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
