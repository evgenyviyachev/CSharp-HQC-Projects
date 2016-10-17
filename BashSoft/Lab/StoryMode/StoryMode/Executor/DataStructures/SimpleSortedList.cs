﻿using Executor.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Executor.DataStructures
{
    public class SimpleSortedList<T> : ISimpleOrderedBag<T>
        where T : IComparable<T>
    {
        private const int DefaultSize = 16;

        private T[] innerCollection;
        private int size;
        private Comparison<T> comparison;

        public SimpleSortedList(Comparison<T> comparison, int capacity)
        {
            this.comparison = comparison;
            this.InitializeInnerCollection(capacity);
        }

        public SimpleSortedList(int capacity)
            : this((x, y) => x.CompareTo(y), capacity)
        {
        }

        public SimpleSortedList(Comparison<T> comparison)
            : this(comparison, DefaultSize)
        {
        }

        public SimpleSortedList()
            : this((x, y) => x.CompareTo(y), DefaultSize)
        {
        }

        private void InitializeInnerCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative!");
            }

            this.innerCollection = new T[capacity];
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }

        public int Capacity
        {
            get
            {
                return this.innerCollection.Length;
            }
        }

        public bool Remove(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            bool hasBeenRemoved = false;
            int indexOfRemovedElement = 0;

            for (int i = 0; i < this.Size; i++)
            {
                if (this.innerCollection[i].Equals(element))
                {
                    indexOfRemovedElement = i;
                    this.innerCollection[i] = default(T);
                    hasBeenRemoved = true;
                    break;
                }
            }

            if (hasBeenRemoved)
            {
                for (int i = indexOfRemovedElement; i < this.Size - 1; i++)
                {
                    this.innerCollection[i] = this.innerCollection[i + 1];
                }

                this.innerCollection[this.Size - 1] = default(T);
                this.size--;
            }

            return hasBeenRemoved;
        }

        public void Add(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            if (this.innerCollection.Length <= this.size)
            {
                this.Resize();
            }

            this.innerCollection[this.size] = element;
            this.size++;

            IComparer<T> comparer = Comparer<T>.Create(this.comparison);
            this.BubbleSort<T>(this.innerCollection, 0, this.size - 1, comparer);
        }

        private void Resize()
        {
            T[] newCollection = new T[this.Size * 2];
            Array.Copy(this.innerCollection, newCollection, this.Size);
            this.innerCollection = newCollection;
        }

        public void AddAll(ICollection<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Size + elements.Count >= this.innerCollection.Length)
            {
                this.MultiResize(elements);
            }

            foreach (var element in elements)
            {
                this.innerCollection[this.size] = element;
                this.size++;
            }

            IComparer<T> comparer = Comparer<T>.Create(this.comparison);
            this.QuickSort<T>(this.innerCollection, 0, this.size - 1, comparer);
        }
        
        private void BubbleSort<T>(T[] array, int index, int length, IComparer<T> comparer)
            where T : IComparable<T>
        {
            bool swapped;
            int indexOfLastUnsortedElement = length;

            do
            {
                swapped = false;

                for (int i = index; i < indexOfLastUnsortedElement; i++)
                {
                    if (comparer.Compare(array[i], array[i + 1]) > 0)
                    {
                        T temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

                indexOfLastUnsortedElement--;
            } while (swapped);
        }
        
        private void QuickSort<T>(T[] data, int l, int r, IComparer<T> comparer)
            where T : IComparable<T>
        {
            int i = l;
            int j = r;
            T x = data[(l + r) / 2];

            while (true)
            {
                while (comparer.Compare(data[i], x) < 0)
                {
                    i++;
                }

                while (comparer.Compare(x, data[j]) < 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    T temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                }

                if (i > j)
                {
                    break;
                }
            }

            if (l < j)
            {
                QuickSort<T>(data, l, j, comparer);
            }

            if (i < r)
            {
                QuickSort<T>(data, i, r, comparer);
            }
        }

        private void MultiResize(ICollection<T> elements)
        {
            int newSize = this.innerCollection.Length * 2;

            while (this.Size + elements.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];
            Array.Copy(this.innerCollection, newCollection, this.Size);
            this.innerCollection = newCollection;
        }

        public string JoinWith(string joiner)
        {
            if (joiner == null)
            {
                throw new ArgumentNullException();
            }

            StringBuilder sb = new StringBuilder();
            foreach (var element in this)
            {
                sb.Append(element);
                sb.Append(joiner);
            }

            sb.Remove(sb.Length - joiner.Length, joiner.Length);
            return sb.ToString();
        }

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < this.Size)
                {
                    return this.innerCollection[index];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                yield return this.innerCollection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
