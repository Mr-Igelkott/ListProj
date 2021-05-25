using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace List.Tests
{
    class LinkedListTests : GlobalTest
    {
        public override void Init(int[] actualArray, int[] expectedArray)
        {
            _actual = LinkedList.Create(actualArray);
            _expected = LinkedList.Create(expectedArray);
        }

        public override void Init(int[] actualArray)
        {
            _actual = LinkedList.Create(actualArray);
        }
    }
}
