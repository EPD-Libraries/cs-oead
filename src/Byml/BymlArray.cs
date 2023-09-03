using System.Collections;

namespace CsOead;

public unsafe class BymlArray : SafeHandleZeroOrMinusOneIsInvalid, IEnumerable<Byml>, IEnumerable
{
    private bool _isChildNode = true;
    private BymlArray() : base(true) { }

    public BymlArray(bool _ = true) : base(true)
    {
        handle = BymlArrayNative.BymlArrayNew();
        _isChildNode = false;
    }

    public static implicit operator BymlArray(Byml[] values) => new(values);

    public BymlArray(params Byml[] values) : this((IEnumerable<Byml>)values) { }
    public BymlArray(IEnumerable<Byml> values) : this(true)
    {
        foreach (var value in values) {
            BymlArrayNative.BymlArrayAdd(this, value);
        }
    }

    public int Length => BymlArrayNative.BymlArrayLength(this);

    public Byml this[int index] {
        get {
            if (index >= Length) {
                throw new IndexOutOfRangeException();
            }

            return BymlArrayNative.BymlArrayGet(this, index);
        }
        set => BymlArrayNative.BymlArraySet(this, index, value);
    }

    public void Add(Byml value)
    {
        BymlArrayNative.BymlArrayAdd(this, value);
    }

    public void Remove(Byml value)
    {
        BymlArrayNative.BymlArrayRemove(this, value);
    }

    public void RemoveAt(int index)
    {
        BymlArrayNative.BymlArrayRemoveAt(this, index);
    }

    public void Clear()
    {
        BymlArrayNative.BymlArrayClear(this);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Byml> GetEnumerator()
    {
        return new Enumerator(this);
    }

    public struct Enumerator : IEnumerator<Byml>, IEnumerator
    {
        private readonly BymlArray _array;
        private readonly int _length;
        private int _index;

        public Enumerator(BymlArray array)
        {
            _array = array;
            _length = _array.Length;
            _index = -1;
        }

        object IEnumerator.Current => Current;
        public Byml Current {
            get => BymlArrayNative.BymlArrayGet(_array, _index);
        }

        public bool MoveNext()
        {
            return ++_index < _length;
        }

        public void Reset()
        {
            _index = -1;
        }

        public void Dispose() { }
    }

    protected override bool ReleaseHandle()
    {
        return IsClosed || _isChildNode || BymlArrayNative.BymlArrayFree(this);
    }
}
