using System;
using System.Collections.Generic;
using System.Text;

namespace List
{
    public interface IList
    {
        int Length { get; }

        int this[int index] { get; set; }

        void Add(int value);

        void Add(IList obj);

        void AddToStart(int value);

        void AddToStart(IList obj);

        void AddByIndex(int index, int value);

        void AddByIndex(int index, IList obj);

        void Remove();

        void Remove(int count);

        void RemoveAtStart();

        void RemoveAtStart(int count);

        void RemoveByIndex(int index);

        void RemoveByIndex(int index, int count);

        int FindIndexOf(int value);

        void Reverse();

        int FindIndexOfMaxElement();

        int FindIndexOfMinElement();

        int FindMaxElement();

        int FindMinElement();

        void RemoveFirstValue(int value);

        void RemoveAllValues(int value);

        void Sort(bool isDecending);

        int[] ToArray();

        string ToString();

        bool Equals(object obj);
    }
}