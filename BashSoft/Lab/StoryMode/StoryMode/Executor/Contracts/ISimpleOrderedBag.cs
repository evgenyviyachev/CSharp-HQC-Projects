using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Executor.Contracts
{
    public interface ISimpleOrderedBag<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        bool Remove(T element);

        int Capacity { get; }

        void Add(T element);

        void AddAll(ICollection<T> elements);

        int Size { get; }

        T this[int index] { get; }

        string JoinWith(string joiner);
    }
}
