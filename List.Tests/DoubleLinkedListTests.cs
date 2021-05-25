using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace List.Tests
{
    class DoubleLinkedListTests : GlobalTest
    {
        public override void Init(int[] actualArray, int[] expectedArray)
        {
            _actual = DoubleLinkedList.Create(actualArray);
            _expected = DoubleLinkedList.Create(expectedArray);
        }

        public override void Init(int[] actualArray)
        {
            _actual = DoubleLinkedList.Create(actualArray);
        }
    }
}
