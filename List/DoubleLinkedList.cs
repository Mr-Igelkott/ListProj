using System;
using System.Collections.Generic;
using System.Text;

namespace List
{
    public class DoubleLinkedList: IList
    {
        private DNode _root;
        private DNode _tail;
        public int Length { get; private set; }

        public int this[int index]
        {
            get
            {
                if (index >= 0 && index < Length)
                {
                    return GetDNodeByIndex(index).Value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            set
            {
                if (index >= 0 && index < Length)
                {
                    GetDNodeByIndex(index).Value = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public DoubleLinkedList()
        {
            Length = 0;
            _root = null;
            _tail = null;
        }

        public DoubleLinkedList(int value)
        {
            Length = 1;
            _root = new DNode(value);
            _tail = _root;
        }

        public static DoubleLinkedList Create(int[] array)
        {
            if (array != null)
            {
                return new DoubleLinkedList(array);
            }
            throw new ArgumentNullException("Array can't be null");
        }
       
        public void Add(int value)
        {
            if (Length > 0)
            {
                Length++;
                _tail.Next = new DNode(value, _tail);
                _tail = _tail.Next;
            }
            else
            {
                Length = 1;
                _root = new DNode(value);
                _tail = _root;
            }
        }

        public void Add(IList list)
        {
            if (list != null)
            {
                DoubleLinkedList addList = DoubleLinkedList.Create(list.ToArray());

                if (list.Length == 0)
                {
                    return;
                }
                if (Length > 0)
                {
                    this._tail.Next = addList._root;
                    addList._root.Previous = this._tail;
                    this._tail = addList._tail;
                    Length += list.Length;
                }
                else
                {
                    this._root = addList._root;
                    this._tail = addList._tail;
                    Length = addList.Length;
                }
            }
            else
            {
                throw new ArgumentNullException("Can't add null value");
            }
        }

        public void AddToStart(int value)
        {
            DNode newRoot = new DNode(value);

            if (Length > 0)
            {
                _root.Previous = newRoot;
                newRoot.Next = _root;
                _root = newRoot;
            }
            else
            {
                _root = newRoot;
                _tail = _root;
            }

            ++Length;
        }

        public void AddToStart(IList list)
        {
            if (list != null)
            {
                DoubleLinkedList addList = DoubleLinkedList.Create(list.ToArray());

                if (Length > 0)
                {
                    if (list.Length > 0)
                    {
                        addList._tail.Next = this._root;
                        this._root.Previous = addList._tail;
                        _root = addList._root;
                    }
                }
                else
                {
                    _root = addList._root;
                    _tail = addList._tail;
                }
                Length += addList.Length;
            }
            else
            {
                throw new ArgumentNullException("Can't add null value");
            }
        }

        public void AddByIndex (int index, int value)
        {
            if ((index == 0 && Length == 0) || (index >= 0 && index < Length))
            {
                if (index != 0)
                {
                    DNode newNode = new DNode(value);
                    DNode current = GetDNodeByIndex(index);
                    newNode.Next = current;
                    newNode.Previous = current.Previous;
                    current.Previous.Next = newNode;
                    current.Previous = newNode;
                    ++Length;
                }
                else
                {
                    AddToStart(value);
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void AddByIndex(int index, IList list)
        {
            if (list != null)
            {

                if ((index == 0 && Length == 0) || (index >= 0 && index < Length))
                {
                    DoubleLinkedList addList = DoubleLinkedList.Create(list.ToArray());

                    if (index != 0)
                    {
                        if (addList.Length > 0)
                        {
                            DNode prev = GetDNodeByIndex(index - 1);
                            DNode next = GetDNodeByIndex(index);
                            prev.Next = addList._root;
                            addList._root.Previous = prev;
                            addList._tail.Next = next;
                            next.Previous = addList._tail;
                            Length += addList.Length;
                        }
                    }
                    else
                    {
                        AddToStart(addList);
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException("Can't add null value");
            }
            
            
        }

        public void Remove()
        {
            if (Length > 1)
            {
                DNode atEnd = GetDNodeByIndex(Length - 2);
                _tail = atEnd;
                _tail.Next = null;
                Length--;
            }
            else
            {
                _root = null;
                _tail = null;
                Length = 0;
            }
        }

        public void Remove(int nElement)
        {
            if (nElement <= Length)
            {
                if (nElement < Length)
                {
                    int index = Length - nElement - 1;
                    DNode current = GetDNodeByIndex(index);
                    current.Next = null;
                    _tail = current;
                    Length -= nElement;
                }
                else
                {
                    Length = 0;
                    _root = null;
                    _tail = null;
                }
            }
            else
            {
                throw new ArgumentException("nElement can't be bigger than overall length");
            }
        }

        public void RemoveAtStart()
        {
            if (Length > 1)
            {
                _root.Next.Previous = null;
                _root = _root.Next;
                Length--;
            }
            else
            {
                _root = null;
                _tail = null;
                Length = 0;
            }
        }

        public void RemoveAtStart(int nElement)
        {
            if (nElement <= Length)
            {
                if (nElement < Length)
                {
                    DNode current = GetDNodeByIndex(nElement);
                    current.Previous = null;
                    _root = current;

                    Length -= nElement;
                }
                else
                {
                    Length = 0;
                    _root = null;
                    _tail = null;
                }
            }
            else
            {
                throw new ArgumentException("nElement can't be bigger than overall length");
            }
        }

        public void RemoveByIndex(int index)
        {
            if ((index == 0 && Length == 0) || (index >= 0 && index < Length))
            {
                if (index != 0)
                {
                    DNode curent = GetDNodeByIndex(index - 1);
                    curent.Next = curent.Next.Next;
                    if (curent.Next is null)
                    {
                        _tail = curent;
                    }
                    else
                    {
                        curent.Next.Previous = curent;
                    }
                    Length--;
                }
                else
                {
                    RemoveAtStart();
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void RemoveByIndex(int index, int nElement)
        {
            if (nElement >= 0)
            {
                if(index >= 0 && index < Length)
                {
                    if (Length != 0 && nElement != 0)
                    {
                        if (index != 0)
                        {
                            if (Length - index - nElement > 0)
                            {
                                DNode startDelete = GetDNodeByIndex(index - 1);
                                DNode stopDelete = GetDNodeByIndex(index + nElement);

                                startDelete.Next = stopDelete;
                                stopDelete.Previous = startDelete;
                                Length -= nElement;
                            }
                            else
                            {
                                DNode lastSurvive = GetDNodeByIndex(index - 1);
                                lastSurvive.Next = null;
                                _tail = lastSurvive;
                                Length = index;
                            }
                        }
                        else
                        {
                            RemoveAtStart(nElement);
                        }
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int FindIndexOf(int value)
        {
            DNode curent = _root;
            for (int i = 0; i < Length; ++i)
            {
                if (curent.Value == value)
                {
                    return i;
                }
                curent = curent.Next;
            }

            return -1;
        }

        public void Reverse()
        {
            if (this != null)
            {
                if (Length > 1)
                {
                    DNode temp = _root.Next;
                    _root.Previous = temp;
                    _root.Next = null;

                    while (temp != _tail)
                    {
                        DNode tmp = temp.Next;
                        temp.Next = temp.Previous;
                        temp.Previous = tmp;
                        temp = temp.Previous;
                    }
                    temp.Next = temp.Previous;
                    temp.Previous = null;
                    _tail = _root;
                    _root = temp;
                }
            }
        }

        public int FindIndexOfMaxElement()
        {
            if (Length > 0)
            {
                int max = _root.Value;
                int index = 0;
                DNode tmp = _root.Next;
                for (int i = 1; i < Length; ++i)
                {
                    if (tmp.Value > max)
                    {
                        index = i;
                        max = tmp.Value;
                    }
                    tmp = tmp.Next;
                }
                return index;
            }
            else
            {
                throw new ArgumentException("");
            }
        }

        public int FindIndexOfMinElement()
        {
            if (Length > 0)
            {
                int min = _root.Value;
                int index = 0;
                DNode tmp = _root.Next;
                for (int i = 1; i < Length; ++i)
                {
                    if (tmp.Value < min)
                    {
                        index = i;
                        min = tmp.Value;
                    }
                    tmp = tmp.Next;
                }
                return index;
            }
            else
            {
                throw new ArgumentException("");
            }
        }

        public int FindMaxElement()
        {
            if (Length > 0)
            {
                DNode curent = _root.Next;
                int max = _root.Value;
                for (int i = 1; i < Length; ++i)
                {
                    if (curent.Value > max)
                    {
                        max = curent.Value;
                    }
                    curent = curent.Next;
                }
                return max;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int FindMinElement()
        {
            if (Length > 0)
            {
                DNode curent = _root.Next;
                int min = _root.Value;
                for (int i = 1; i < Length; ++i)
                {
                    if (curent.Value < min)
                    {
                        min = curent.Value;
                    }
                    curent = curent.Next;
                }
                return min;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void RemoveFirstValue(int value)
        {
            int index = FindIndexOf(value);
            if (index >= 0)
            {
                RemoveByIndex(index);
            }
        }

        public void RemoveAllValues(int value)
        {
            int index = FindIndexOf(value);

            while (index >= 0)
            {
                RemoveByIndex(index);
                index = FindIndexOf(value);
            }
        }

        public void Sort(bool isAscending)
        {
            if (this != null)
            {
                DNode new_root = null;
                
                while(_root != null)
                {
                    DNode tmp = _root;
                    _root = _root.Next;

                    if (new_root == null || (tmp.Value < new_root.Value && isAscending)
                        || (tmp.Value > new_root.Value && !isAscending))
                    {
                        tmp.Next = new_root;
                        tmp.Previous = null;

                        if (tmp.Next == null)
                        {
                            _tail = tmp;
                        }
                        else
                        {
                            tmp.Next.Previous = tmp;
                        }

                        new_root = tmp;
                    }
                    else
                    {
                        DNode current = new_root;

                        while (((current.Next != null) && !(tmp.Value < current.Next.Value) && isAscending)
                            || ((current.Next != null) && !(tmp.Value > current.Next.Value) && !isAscending))
                        {
                            current = current.Next;
                        }
                        tmp.Next = current.Next;

                        if(tmp.Next == null)
                        {
                            _tail = tmp;
                        }
                        else
                        {
                            tmp.Next.Previous = tmp;
                        }

                        current.Next = tmp;
                        tmp.Previous = current;
                    }
                }

                _root = new_root;
            }
        }

        public int[] ToArray()
        {
            int[] array = new int[Length];
            int count = 0;
            DNode current = _root;
            while (current != null)
            {
                array[count] = current.Value;
                count++;
                current = current.Next;
            }

            return array;
        }

        public override string ToString()
        {
            DNode current = _root;
            StringBuilder stringBuilder = new StringBuilder();

            while(current != null)
            {
                stringBuilder.Append($"{current.Value} ");
                current = current.Next;
            }

            return stringBuilder.ToString().Trim();
        }

        public override bool Equals(object obj)
        {
            if(obj != null)
            {
                DoubleLinkedList list = (DoubleLinkedList)obj;
                bool isEqual = false;

                if (this.Length == list.Length)
                {
                    isEqual = true;

                    DNode currentThis = this._root;
                    DNode currentList = list._root;
                    DNode prevThis = this._tail;
                    DNode prevList = list._tail;

                    while(currentThis != null)
                    {
                        if(currentThis.Value != currentList.Value || prevThis.Value != prevList.Value)
                        {
                            isEqual = false;
                            break;
                        }
                        currentThis = currentThis.Next;
                        currentList = currentList.Next;
                        prevThis = prevThis.Previous;
                        prevList = prevList.Previous;
                    }
                }
                return isEqual;
            }
            throw new ArgumentException("Object can't be NULL");
        }

        private DoubleLinkedList(int[] array)
        {
            if (array != null)
            {
                if (array.Length != 0)
                {
                    _root = new DNode(array[0]);
                    _tail = _root;
                    Length = 1; ;

                    for (int i = 1; i < array.Length; i++)
                    {
                        Add(array[i]);
                    }
                }
                else
                {
                    _root = null;
                    _tail = null;
                    Length = 0;
                }
            }
            else
            {
                throw new ArgumentNullException("Object can't be NULL");
            }
        }

        private DNode GetDNodeByIndex(int index)
        {
            DNode current;
            if (index < Length / 2)
            {
                current = _root;
                for (int i = 1; i <= index; i++)
                {
                    current = current.Next;
                }
            }
            else
            {
                current = _tail;
                int indexBack = Length - 1 - index;
                for (int i = 1; i <= indexBack; ++i)
                {
                    current = current.Previous;
                }
            }
            return current;
        }
    }
}
