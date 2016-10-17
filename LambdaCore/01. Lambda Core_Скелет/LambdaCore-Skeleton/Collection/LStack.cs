namespace LambdaCore_Skeleton.Collection
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class LStack
    {
        private LinkedList<IFragment> innerList;

        public LStack()
        {
            this.innerList = new LinkedList<IFragment>();
        }

        public int Count()
        {
            return this.innerList.Count;
        }

        public IFragment Push(IFragment item)
        {
            this.innerList.AddLast(item);
            return item;
        }

        public void Pop()
        {
            this.innerList.RemoveLast();
        }

        public IFragment Peek()
        {
            IFragment peekedItem = this.innerList.Last();
            return peekedItem;
        }

        public bool IsEmpty()
        {
            return this.innerList.Count == 0;
        }
    }
}
