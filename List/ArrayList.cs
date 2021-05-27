using System;
using System.Text;

namespace List
{
    public class ArrayList : IList
    {
        public int Length { get; private set; }
        const double LengthDelta = 1.33d;

        private int[] _array;

        public int this[int index]
        {
            get
            {
                if (index >= 0 && index < Length)
                {
                    return _array[index];
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
                    _array[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public ArrayList()
        {
            Length = 0;
            _array = new int[10];
        }

        public ArrayList(int value)
        {
            Length = 1;
            _array = new int[10];
            _array[0] = value;
        }

        public static ArrayList Create(int[] initArray)
        {
            if (initArray != null)
            {
                return new ArrayList(initArray);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private ArrayList(int[] initArray)
        {
            Length = initArray.Length;
            int realLength = (int)(Length * LengthDelta + 1);
            _array = new int[realLength];

            for (int i = 0; i < Length; i++)
            {
                _array[i] = initArray[i];
            }
        }

        public void Add(int value)
        {
            this.Resize(Length);
            _array[Length] = value;
            Length++;
        }

        public void Add(IList addList)
        {
            if (addList != null)
            {
                ArrayList list = Create(addList.ToArray());
                int prevLength = Length;
                Length += list.Length;
                this.Resize(prevLength);
                for (int i = 0; i < list.Length; i++)
                {
                    _array[prevLength + i] = list[i];
                }
            }
            else
            {
                throw new ArgumentNullException("Can't add NULL value");
            }
        }

        public void AddToStart(int value)
        {
            Length++;
            Resize(Length);
            MoveRight(0, 1);
            _array[0] = value;
        }

        public void AddToStart(IList addList)
        {
            if (addList != null)
            {
                AddByIndex(0, addList);
            }
            else
            {
                throw new ArgumentNullException("Can't add NULL value");
            }
        }

        public void AddByIndex(int index, int value)
        {
            if ((index == 0 && Length == 0) || (index >= 0 && index < Length))
            {
                Resize(Length);
                Length++;
                MoveRight(index, 1);
                _array[index] = value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void AddByIndex(int index, IList addList)
        {
            if (addList != null)
            {
                if ((index == 0 && Length == 0) || (index >= 0 && index < Length))
                {
                    ArrayList list = ArrayList.Create(addList.ToArray());
                    int prevLength = Length;
                    Length += list.Length;
                    Resize(prevLength);
                    int moveIndex = index + list.Length - 1;
                    MoveRight(moveIndex, list.Length);

                    for (int i = 0; i < list.Length; ++i)
                    {
                        _array[i + index] = list[i];
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException("Can't add NULL value");
            }
        }

        public void Remove()
        {
            if (Length > 0)
            {
                Length--;
                Resize(Length);
            }
        }

        public void Remove(int nElement)
        {
            if (nElement >= 0)
            {
                Length -= Length >= nElement ? nElement : Length;
                Resize(Length);
            }
            else
            {
                throw new ArgumentException("Can't remove negative nElement");
            }
        }

        public void RemoveAtStart()
        {
            if (Length > 0)
            {
                MoveLeft(0, 1);
                Length--;
                Resize(Length);
            }
        }

        public void RemoveAtStart(int nElement)
        {
            if (nElement >= 0)
            {
                Length -= Length >= nElement ? nElement : Length;
                MoveLeft(0, nElement);
                Resize(Length);
            }
            else
            {
                throw new ArgumentException("Can't remove negative nElement");
            }
        }

        public void RemoveByIndex(int index)
        {
            if ((index == 0 && Length == 0) || (index >= 0 && index < Length))
            {
                if (Length != 0)
                {
                    Length--;
                    MoveLeft(index, 1);
                    Resize(Length);
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
                if ((index == 0 && Length == 0) || (index >= 0 && index < Length))
                {
                    if ((Length - index) >= nElement)
                    {
                        Length -= nElement;
                        MoveLeft(index, nElement);
                    }
                    else if ((Length - index) > 0)
                    {
                        Length = index;
                    }
                    else
                    {
                        Length = 0;
                    }
                    Resize(Length);
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentException("Can't remove negative nElement");
            }
        }

        public int FindIndexOf(int value)
        {
            for (int i = 0; i < Length; i++)
            {
                if (_array[i] == value)
                {
                    return i;
                }
            }

            return -1;
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
            for (int i = 0; i < Length; ++i)
            {
                if (_array[i] == value)
                {
                    RemoveByIndex(i);
                    i--;
                }
            }
        }

        public void Reverse()
        {
            for (int i = 0; i < Length / 2; i++)
            {
                int temp = _array[i];
                int index = Length - 1 - i;
                _array[i] = _array[index];
                _array[index] = temp;
            }
        }

        public int FindIndexOfMaxElement()
        {
            if (_array != null)
            {
                if (Length > 0)
                {
                    int max = _array[0];
                    int index = 0;
                    for (int i = 1; i < Length; ++i)
                    {
                        if (_array[i] > max)
                        {
                            max = _array[i];
                            index = i;
                        }
                    }
                    return index;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public int FindIndexOfMinElement()
        {
            if (_array != null)
            {
                if (Length > 0)
                {
                    int min = _array[0];
                    int index = 0;
                    for (int i = 0; i < Length; ++i)
                    {
                        if (_array[i] < min)
                        {
                            min = _array[i];
                            index = i;
                        }
                    }
                    return index;
                }
                throw new ArgumentException();
            }
            throw new ArgumentNullException();
        }

        public int FindMaxElement()
        {
            return _array[FindIndexOfMaxElement()];
        }

        public int FindMinElement()
        {
            return _array[FindIndexOfMinElement()];
        }

        public void Sort(bool isAscending)
        {
            if (_array != null)
            {
                for (int i = 0; i < Length; ++i)
                {
                    int indexMax = i;
                    for (int j = i; j < Length; ++j)
                    {
                        if ((_array[j] < _array[indexMax] && isAscending)
                            || (_array[j] > _array[indexMax] && !isAscending))
                        {
                            indexMax = j;
                        }
                    }
                    Swap(i, indexMax);
                }
            }
        }

        public int[] ToArray()
        {
            int[] array = new int[Length];
            for (int i = 0; i < Length; ++i)
            {
                array[i] = _array[i];
            }
            return array;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < Length; ++i)
            {
                stringBuilder.Append($"{_array[i]} ");
            }
            return stringBuilder.ToString().Trim();
        }

        public override bool Equals(object obj)
        {
            if (obj is ArrayList)
            {
                ArrayList list = (ArrayList)obj;
                if (this.Length != list.Length)
                {
                    return false;
                }
                for (int i = 0; i < Length; ++i)
                {
                    if (this._array[i] != list._array[i])
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                throw new ArgumentException("Error. Wrong type");
            }
        }

        private void MoveRight(int index, int count)
        {
            for (int i = Length - 1; i > index; --i)
            {
                _array[i] = _array[i - count];
            }
        }

        private void MoveLeft(int index, int count)
        {
            for (int i = index; i < Length; ++i)
            {
                _array[i] = _array[i + count];
            }
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            int tmp = _array[firstIndex];
            _array[firstIndex] = _array[secondIndex];
            _array[secondIndex] = tmp;
        }

        private void Resize(int prevLength)
        {
            if ((Length >= _array.Length) || (Length <= _array.Length / 2))
            {
                int newLength = (int)(Length * LengthDelta + 1);
                int[] tmpArray = new int[newLength];

                for (int i = 0; i < prevLength; ++i)
                {
                    tmpArray[i] = _array[i];
                }

                _array = tmpArray;
            }
        }
    }
}
