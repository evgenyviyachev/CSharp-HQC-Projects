namespace BashSoftTesting
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Executor.Contracts;
    using Executor.DataStructures;
    using Microsoft.VisualStudio.TestTools.UnitTesting;    
    
    [TestClass]
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [TestInitialize]
        public void TestInit()
        {
            this.names = new SimpleSortedList<string>();
        }

        [TestMethod]
        public void TestAddIncreaseSize()
        {
            this.names.Add("Nasko");
            Assert.AreEqual(1, this.names.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddNullThrowsException()
        {
            this.names.Add(null);
        }

        [TestMethod]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            Assert.AreEqual("Balkan", this.names[0]);
            Assert.AreEqual("Georgi", this.names[1]);
            Assert.AreEqual("Rosen", this.names[2]);
        }

        [TestMethod]
        public void TestAddMoreThanInitialCapacity()
        {
            for (int i = 0; i < 17; i++)
            {
                this.names.Add(i.ToString());
            }

            Assert.AreEqual(17, this.names.Size);
            Assert.AreNotEqual(16, this.names.Capacity);
        }

        [TestMethod]
        public void TestAddAllFromCollectionIncreaseSize()
        {
            List<string> collection = new List<string>() { "Pesho", "Ivan" };
            this.names.AddAll(collection);

            Assert.AreEqual(2, this.names.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddFromNullThrowsException()
        {
            this.names.AddAll(null);
        }

        [TestMethod]
        public void TestAddAllKeepsSorted()
        {
            this.names.AddAll(new[] { "Rosen", "Georgi", "Balkan" });

            Assert.AreEqual("Balkan", this.names[0]);
            Assert.AreEqual("Georgi", this.names[1]);
            Assert.AreEqual("Rosen", this.names[2]);
        }

        [TestMethod]
        public void TestRemoveValidElementDecreasesSize()
        {
            this.names.AddAll(new[] { "Rosen", "Georgi", "Balkan" });
            this.names.Remove("Georgi");

            Assert.AreEqual(2, this.names.Size);
        }

        [TestMethod]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            this.names.AddAll(new[] { "Rosen", "Georgi", "Balkan" });
            this.names.Remove("Georgi");

            Assert.IsTrue(!this.names.Any(n => n == "Georgi"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemoveNullThrowsException()
        {
            this.names.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestJoinWithNull()
        {
            this.names.JoinWith(null);
        }

        [TestMethod]
        public void TestJoinWithWorksCorrectly()
        {
            this.names.AddAll(new[] { "Rosen", "Georgi", "Balkan" });

            string joinWith = this.names.JoinWith(", ");

            Assert.AreEqual("Balkan, Georgi, Rosen", joinWith);
        }
    }
}
