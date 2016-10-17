using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LambdaCore_Skeleton.Collection;
using LambdaCore_Skeleton.Contracts;

namespace LambdaCoreTests
{
    [TestClass]
    public class LStackTest
    {
        private LStack collection;

        [TestInitialize]
        public void TestInit()
        {
            this.collection = new LStack();
        }

        [TestMethod]
        public void PushOne_CountShouldBecomeOne()
        {
            IFragment fragment = new FakeFragment();

            this.collection.Push(fragment);
            Assert.AreEqual(1, this.collection.Count());
        }

        [TestMethod]
        public void PopOneWhenCountOne_CountShouldBecomeZero()
        {
            IFragment fragment = new FakeFragment();

            this.collection.Push(fragment);
            this.collection.Pop();

            Assert.AreEqual(0, this.collection.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopOneWhenEmptyShouldThrow()
        {
            this.collection.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PeekWhenEmptyShouldThrow()
        {
            this.collection.Peek();
        }

        [TestMethod]
        public void PeekShouldNotChangeCount()
        {
            IFragment fragment = new FakeFragment();

            this.collection.Push(fragment);
            int count1 = this.collection.Count();
            fragment = this.collection.Peek();
            int count2 = this.collection.Count();

            Assert.AreEqual(count1, count2);
        }

        [TestMethod]
        public void WhenIsEmptyShouldReturnTrue()
        {
            Assert.IsTrue(this.collection.IsEmpty());
        }
    }
}
