using NUnit.Framework;
using System;

namespace List.Tests
{
    public abstract class GlobalTest
    {
        public abstract void Init(int[] actualArray, int[] expectedArray);
        public abstract void Init(int[] actualArray);

        protected IList _actual;
        protected IList _expected;

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, "1 2 3 4 5 6")]
        [TestCase(new int[] { }, "")]
        public void ToString_WhenIntPassed_ThenReturnString(int[] actualArray, string expected)
        {
            Init(actualArray);
            Assert.AreEqual(expected, _actual.ToString());
        }

        [TestCase(new int[] { 1 }, -1)]
        [TestCase(new int[] { 1, 2 }, 6)]
        public void SetValueByIndex_WhenIncorrectIndexPassed_ThenReturnIndexOutOfRangeException(int[] actualArray, int value)
        {
            Init(actualArray);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                _actual[value] = 11;
            });
        }
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, new int[] { 1, 2, 3, 4, 5, 6 })]
        [TestCase(new int[] { }, new int[] { })]
        public void GetArray_WhenArrayPassed_ThenReturnArray(int[] actualArray, int[] expectedArray)
        {
            Init(actualArray);

            Assert.AreEqual(expectedArray, _actual.ToArray());
        }

        [TestCase(new int[] { 1 }, -1)]
        [TestCase(new int[] { 1, 2 }, 6)]
        public void GetValueByIndex_WhenIncorrectIndexPassed_ThenReturnIndexOutOfRangeException(int[] actualArray, int index)
        {
            Init(actualArray);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                int value = _actual[index];
            });
        }

        [TestCase(new int[] { 1 }, 0, 1)]
        [TestCase(new int[] { 1, 2 }, 1, 2)]
        public void GetValueByIndex_WhenCorrectIndexPassed_ThenReturnValue(int[] actualArray, int index, int expected)
        {
            Init(actualArray);

            int actual = _actual[index];

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { 1 }, -1, -1)]
        [TestCase(new int[] { 1, 2 }, 6, 6)]
        public void SetValueByIndex_WhenCorrectIndexPassed_ThenSetValue(int[] actualArray, int value, int expected)
        {
            Init(actualArray);

            _actual[0] = value;

            Assert.AreEqual(expected, _actual[0]);
        }

        [TestCase(new int[] { 1, 2, 3 }, 4, new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { 1 }, 4, new int[] { 1, 4 })]
        [TestCase(new int[] { }, 4, new int[] { 4 })]
        public void Add_WhenValuePassed_ThenAdd(int[] actualArray, int value, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.Add(value);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1 }, new int[] { 4, 5, 6 }, new int[] { 1, 4, 5, 6 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 1, 2, 3, 4, 5, 6 })]
        [TestCase(new int[] { }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { }, new int[] { }, new int[] { })]
        public void Add_WhenListPassed_ThenAddList(int[] actualArray, int[] arrayForList, int[] expectedArray)
        {
            Init(actualArray, expectedArray);

            _actual.Add(DoubleLinkedList.Create(arrayForList));


            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { })]
        public void Add_WhenNullPassed_ThenReturnArgumentException(int[] actualArray)
        {
            Init(actualArray);

            Assert.Throws<ArgumentNullException>(() =>
            {
                _actual.Add(null);
            });
        }

        [TestCase(new int[] { 1, 2, 3 }, 0, new int[] { 0, 1, 2, 3 })]
        [TestCase(new int[] { }, 0, new int[] { 0 })]
        [TestCase(new int[] { 1 }, 0, new int[] { 0, 1 })]
        public void AddToStart_WhenValuePassed_ThenAddToStart(int[] actualArray, int value, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.AddToStart(value);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 4, 5, 6, 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 4 }, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3, 4 })]
        [TestCase(new int[] { 1 }, new int[] { 0 }, new int[] { 0, 1 })]
        public void AddToStart_WhenListPassed_ThenAddListToStart(int[] actualArray, int[] arrayForList, int[] expectedArray)
        {
            Init(actualArray, expectedArray);

            _actual.AddToStart(DoubleLinkedList.Create(arrayForList));

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { })]
        public void AddToStart_WhenNullPassed_ThenReturnArgumentException(int[] actualArray)
        {
            Init(actualArray);
            Assert.Throws<ArgumentNullException>(() =>
            {
                _actual.AddToStart(null);
            });
        }

        [TestCase(new int[] { }, 0, 10, new int[] { 10 })]
        [TestCase(new int[] { 1 }, 0, 10, new int[] { 10, 1 })]
        [TestCase(new int[] { 1, 2 }, 1, 10, new int[] { 1, 10, 2 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, 5, 10, new int[] { 1, 2, 3, 4, 5, 10, 6 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, 1, 10, new int[] { 1, 10, 2, 3, 4, 5, 6 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 }, 0, 10, new int[] { 10, 1, 2, 3, 4, 5, 6 })]
        public void AddByIndex_WhenValueAndIndexPassed_ThenAddByIndex(int[] actualArray, int index, int value, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.AddByIndex(index, value);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, -1)]
        [TestCase(new int[] { 1, 2, 3 }, 3)]
        public void AddByIndex_WhenIncorrectIndexPassed_ThenReturnIndexOutOfRange(int[] actualArray, int index)
        {
            Init(actualArray);
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                _actual.AddByIndex(index, 0);

            });
        }

        [TestCase(new int[] { }, 0, new int[] { }, new int[] { })]
        [TestCase(new int[] { 2 }, 0, new int[] { 1 }, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 3 }, 1, new int[] { 2 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 2, new int[] { 4, 5, 6 }, new int[] { 1, 2, 4, 5, 6, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 0, new int[] { 4, 5, 6 }, new int[] { 4, 5, 6, 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 1, new int[] { 4, 5, 6 }, new int[] { 1, 4, 5, 6, 2, 3 })]
        [TestCase(new int[] { }, 0, new int[] { 4, 5, 6 }, new int[] { 4, 5, 6 })]
        [TestCase(new int[] { 1, 2, 3 }, 0, new int[] { }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 2, new int[] { }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { }, 0, new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 10, 11, 12, 12 }, 10, new int[] { 1, 2 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 10, 11, 12, 12 })]
        public void AddByIndex_WhenValidDataPassed_ThenAddByIndex(int[] actualArray, int index, int[] arrayForList, int[] expectedArray)
        {
            Init(actualArray, expectedArray);

            _actual.AddByIndex(index, DoubleLinkedList.Create(arrayForList));

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, -1, new int[] { 4, 5, 6 })]
        [TestCase(new int[] { 1, 2, 3 }, 3, new int[] { 4, 5, 6 })]
        public void AddByIndex_WhenIncorrectIndexPassed_ThenReturnIndexOutOfRangeException(int[] actualArray, int index, int[] arrayForList)
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                _actual.AddByIndex(index, DoubleLinkedList.Create(arrayForList));
            });
        }

        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2 })]
        [TestCase(new int[] { }, new int[] { })]
        [TestCase(new int[] { 1 }, new int[] { })]
        public void Remove_WhenMethodCalled_ThenRemoveLastElement(int[] actualArray, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.Remove();

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, new int[] { 2, 3 })]
        [TestCase(new int[] { 1 }, new int[] { })]
        [TestCase(new int[] { }, new int[] { })]
        public void RemoveAtStart_WhenMethodCalled_ThenRemoveFirstElement(int[] actualArray, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.RemoveAtStart();

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, 0, new int[] { 2, 3 })]
        [TestCase(new int[] { 1, 2, 3, 4 }, 2, new int[] { 1, 2, 4 })]
        [TestCase(new int[] { 1, 2, 3 }, 2, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2 }, 1, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, 1, new int[] { 1 })]
        [TestCase(new int[] { 1 }, 0, new int[] { })]
        [TestCase(new int[] { }, 0, new int[] { })]
        public void RemoveByIndex_WhenIndexPassed_ThenRemoveByIndex(int[] actualArray, int index, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.RemoveByIndex(index);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, -1)]
        [TestCase(new int[] { 1, 2, 3 }, 3)]
        public void RemoveByIndex_WhenIncorrectIndexPassed_ThenReturnIndexOutOfRangeException(int[] actualArray, int index)
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                Init(actualArray);
                _actual.RemoveByIndex(index);
            });
        }

        [TestCase(new int[] { 1, 2, 3 }, 3, new int[] { })]
        [TestCase(new int[] { 1, 2, 3 }, 2, new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3 }, 1, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2, 3 }, 0, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { }, 0, new int[] { })]
        public void Remove_WhennElmntPassed_ThenRemoveLastnElmnt(int[] actualArray, int nElmnt, int[] expectedArray)
        {
            Init(actualArray, expectedArray);

            _actual.Remove(nElmnt);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, 3, new int[] { })]
        [TestCase(new int[] { 1, 2, 3 }, 2, new int[] { 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 1, new int[] { 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 0, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { }, 0, new int[] { })]
        public void RemoveAtStart_WhennElmntPassed_ThenRemoveFirstnElmntElements(int[] actualArray, int nElmnt, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.RemoveAtStart(nElmnt);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3 }, 0, 3, new int[] { })]
        [TestCase(new int[] { 1, 2, 3 }, 0, 1, new int[] { 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 0, 0, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 2, 0, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 2, 1, new int[] { 1, 2 })]
        [TestCase(new int[] { 1, 2, 3 }, 1, 1, new int[] { 1, 3 })]
        [TestCase(new int[] { 1, 2, 3 }, 1, 2, new int[] { 1 })]
        [TestCase(new int[] { 1, 2 }, 1, 1, new int[] { 1 })]
        [TestCase(new int[] { 1 }, 0, 1, new int[] { })]
        public void RemoveByIndex_WhenValidDataPassed_ThenRemoveByIndex(int[] actualArray, int index, int nElmnt, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.RemoveByIndex(index, nElmnt);
            Assert.AreEqual(_expected, _actual);
        }


        [TestCase(new int[] { 1, 2, 3 }, 0, -1)]
        public void RemoveByIndex_WhenIncorrectnElmntPassed_ThenReturnArgumentException(int[] actualArray, int index, int nElmnt)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Init(actualArray);
                _actual.RemoveByIndex(index, nElmnt);
            });
        }


        [TestCase(new int[] { 1, 2, 3 }, -1, 0)]
        [TestCase(new int[] { 1, 2, 3 }, 6, 0)]
        public void RemoveByIndex_WhenIncorrectIndexPassed_ThenReturnIndexOutOfRangeException(int[] actualArray, int index, int quontity)
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                Init(actualArray);
                _actual.RemoveByIndex(index, quontity);
            });
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new int[] { 8, 7, 6, 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 8, 8, 8, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 9 }, new int[] { 9, 9, 9, 9, 9, 9, 9, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7, 6, 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7 }, new int[] { 7, 6, 5, 4, 3, 2, 1 })]
        [TestCase(new int[] { 1, 2 }, new int[] { 2, 1 })]
        [TestCase(new int[] { 1 }, new int[] { 1 })]
        [TestCase(new int[] { }, new int[] { })]
        public void Reverse_WhenMethodCalled_ThenReverseList(int[] actualArray, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            _actual.Reverse();

            Assert.AreEqual(_expected, _actual);
        }

        [Test]
        public void Reverse_WhenListIsNull_ThenReturnNullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() =>
            {
                DoubleLinkedList actual = null;

                actual.Reverse();
            });
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 7)]
        [TestCase(new int[] { 8, 2, 3, 4, 5, 6, 7 }, 0)]
        [TestCase(new int[] { 1, 2, 2 }, 1)]
        [TestCase(new int[] { 1 }, 0)]
        public void FindIndexOfMaximumElement_WhenMethodCalled_ThenReturnIndexOfMaxElement(int[] actualArray, int expected)
        {
            Init(actualArray);

            int actual = _actual.FindIndexOfMaxElement();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { })]
        public void FindIndexOfMaximumElement_WhenListIsEmptyPassed_ThenReturnArgumentException(int[] actualArray)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Init(actualArray);

                int actual = _actual.FindIndexOfMaxElement();
            });
        }
        [TestCase(null)]
        public void FindIndexOfMaximumElement_WhenNullPassed_ThenReturnArgumentNullException(int[] actualArray)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Init(actualArray);

                int actual = _actual.FindIndexOfMaxElement();
            });
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 0)]
        [TestCase(new int[] { 8, 2, 3, 4, 5, 6, 1 }, 6)]
        [TestCase(new int[] { 2, 2, 2 }, 0)]
        [TestCase(new int[] { 1 }, 0)]
        [TestCase(new int[] { 6, 5, 4, 3, 4, 5, 6 }, 3)]
        public void FindIndexOfMinimumElement_WhenMethodCalled_ThenReturnIndexOfMinimumElement(int[] actualArray, int expected)
        {
            Init(actualArray);

            int actual = _actual.FindIndexOfMinElement();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { })]
        public void FindIndexOfMinimumElement_WhenListIsEmptyPassed_ThenReturnArgumentException(int[] actualArray)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Init(actualArray);

                int actual = _actual.FindIndexOfMinElement();
            });
        }
        [TestCase(null)]
        public void FindIndexOfMinimumElement_WhenNullPassed_ThenReturnNullArgumentException(int[] actualArray)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Init(actualArray);

                int actual = _actual.FindIndexOfMinElement();
            });
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 8)]
        [TestCase(new int[] { 8, 2, 3, 4, 5, 6, 7 }, 8)]
        [TestCase(new int[] { 1, 2, 2 }, 2)]
        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new int[] { 2, 3, 4, 6, 4, 5, 6 }, 6)]
        public void FindMaximumElement_WhenMethodCalled_ThenReturnMaxElement(int[] actualArray, int expected)
        {
            Init(actualArray);

            int actual = _actual.FindMaxElement();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { })]
        public void FindMaximumElement_WhenNoElementsInCollection_ThenReturnArgumentException(int[] actualArray)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Init(actualArray);

                int actual = _actual.FindMaxElement();
            });
        }

        [TestCase(null)]
        public void FindMaximumElement_WhenCollectionIsNull_ThenReturnArgumentNullException(int[] actualArray)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Init(actualArray);

                int actual = _actual.FindMaxElement();
            });
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 1)]
        [TestCase(new int[] { 8, 2, 3, 4, 5, 6, 1 }, 1)]
        [TestCase(new int[] { 2, 2, 2 }, 2)]
        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new int[] { 6, 5, 4, 3, 4, 5, 6 }, 3)]
        public void FindMinimumElement_WhenMethodCalled_ThenReturnMinElement(int[] actualArray, int expected)
        {
            Init(actualArray);

            int actual = _actual.FindMinElement();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(new int[] { })]
        public void FindMinimumElement_WhenNoElementsInCollection_ThenReturnArgumentException(int[] actualArray)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Init(actualArray);

                int actual = _actual.FindMinElement();
            });
        }
        [TestCase(null)]
        public void FindMinimumElement_WhenCollectionIsNull_ThenReturnArgumentNullException(int[] actualArray)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Init(actualArray);

                int actual = _actual.FindMinElement();
            });
        }

        [TestCase(new int[] { 7, 3, 2, 5, 1, 6, 2 }, new int[] { 1, 2, 2, 3, 5, 6, 7 })]
        [TestCase(new int[] { 2, 2, 2, 2, 2, 2, 2 }, new int[] { 2, 2, 2, 2, 2, 2, 2 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 2, 1 }, new int[] { 1, 2, 3 })]
        [TestCase(new int[] { 3, 1, 1 }, new int[] { 1, 1, 3 })]
        [TestCase(new int[] { 3, 3, 1 }, new int[] { 1, 3, 3 })]
        [TestCase(new int[] { 3, 1 }, new int[] { 1, 3 })]
        [TestCase(new int[] { 2, 2 }, new int[] { 2, 2 })]
        [TestCase(new int[] { }, new int[] { })]
        public void Sort_WhenIsAscending_ThenSortAscending(int[] actualArray, int[] expectedArray)
        {
            Init(actualArray, expectedArray);

            bool isAscending = true;

            _actual.Sort(isAscending);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 7, 3, 2, 5, 1, 6, 2 }, new int[] { 7, 6, 5, 3, 2, 2, 1 })]
        [TestCase(new int[] { 2, 2, 2, 2, 2, 2, 2 }, new int[] { 2, 2, 2, 2, 2, 2, 2 })]
        [TestCase(new int[] { 1, 2, 3 }, new int[] { 3, 2, 1 })]
        [TestCase(new int[] { 3, 2, 1 }, new int[] { 3, 2, 1 })]
        [TestCase(new int[] { 1, 1, 3 }, new int[] { 3, 1, 1 })]
        [TestCase(new int[] { 1, 3, 3 }, new int[] { 3, 3, 1 })]
        [TestCase(new int[] { 1, 3 }, new int[] { 3, 1 })]
        [TestCase(new int[] { 2, 2 }, new int[] { 2, 2 })]
        [TestCase(new int[] { }, new int[] { })]
        public void Sort_WhenIsAscendingFalse_ThenSortDescending(int[] actualArray, int[] expectedArray)
        {
            Init(actualArray, expectedArray);
            bool isAscending = false;

            _actual.Sort(isAscending);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 1, new int[] { 2, 3, 4, 5, 6, 7, 8 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 8, new int[] { 1, 2, 3, 4, 5, 6, 7 })]
        [TestCase(new int[] { 1, 1, 3, 4, 5, 6, 8, 8 }, 10, new int[] { 1, 1, 3, 4, 5, 6, 8, 8 })]
        [TestCase(new int[] { 1, 1, 3, 4, 5, 6, 8, 8 }, 8, new int[] { 1, 1, 3, 4, 5, 6, 8 })]
        public void RemoveByValue_WhenValuePassed_ThenRemoveFirstValue(int[] actualArray, int value, int[] expectedArray)
        {
            Init(actualArray, expectedArray);

            _actual.RemoveFirstValue(value);

            Assert.AreEqual(_expected, _actual);
        }

        [TestCase(new int[] { 1, 2, 1, 4, 1, 6, 7, 8 }, 1, new int[] { 2, 4, 6, 7, 8 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 8, new int[] { 1, 2, 3, 4, 5, 6, 7 })]
        [TestCase(new int[] { 1, 1, 3, 4, 5, 6, 8, 8 }, 10, new int[] { 1, 1, 3, 4, 5, 6, 8, 8 })]
        [TestCase(new int[] { 8, 8, 8 }, 8, new int[] { })]
        public void RemoveAllByValues_WhenValuePassed_TnenRemoveAllValues(int[] actualArray, int value, int[] expectedArray)
        {
            Init(actualArray, expectedArray);

            _actual.RemoveAllValues(value);

            Assert.AreEqual(_expected, _actual); ;
        }
    }
}
