using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace CsOead;

public unsafe class BymlHash : SafeHandleZeroOrMinusOneIsInvalid, IEnumerable<KeyValuePair<string, Byml>>, IEnumerable
{
    private readonly bool _isChildNode = true;
    private BymlHash() : base(true) { }

    public BymlHash(bool _ = true) : base(true)
    {
        handle = BymlHashNative.BymlHashNew();
        _isChildNode = false;
    }

    public static implicit operator BymlHash(Dictionary<string, Byml> values) => new(values);
    public static implicit operator BymlHash(SortedDictionary<string, Byml> values) => new(values);
    public BymlHash(IEnumerable<KeyValuePair<string, Byml>> values) : this(true)
    {
        foreach ((var key, var value) in values) {
            BymlHashNative.BymlHashAdd(this, key, value);
        }
    }

    public int Count => BymlHashNative.BymlHashCount(this);

    public Byml this[string key] {
        get => Get(key);
        set => BymlHashNative.BymlHashAdd(this, key, value);
    }

    public Byml Get(string key)
    {
        if (BymlHashNative.BymlHashGet(this, key, out Byml output)) {
            return output;
        }

        throw new KeyNotFoundException(
            $"Could not find an entry with the key '{key}'");
    }

    public bool TryGet(string key, out Byml value)
    {
        bool result = BymlHashNative.BymlHashGet(this, key, out Byml output);
        value = output;
        return result;
    }

    public void Add(string key, Byml value)
    {
        BymlHashNative.BymlHashAdd(this, key, value);
    }

    public void Remove(string key)
    {
        BymlHashNative.BymlHashRemove(this, key);
    }

    public bool ContainsKey(string key)
    {
        return BymlHashNative.BymlHashContainsKey(this, key);
    }

    public void Clear()
    {
        BymlHashNative.BymlHashClear(this);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<KeyValuePair<string, Byml>> GetEnumerator()
    {
        return new Enumerator(this);
    }

    public struct Enumerator : IEnumerator<KeyValuePair<string, Byml>>, IEnumerator
    {
        private readonly BymlHash _hash;
        private IntPtr _iterator;

        object IEnumerator.Current => Current;
        public KeyValuePair<string, Byml> Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                BymlHashNative.BymlHashIteratorCurrent(_iterator, out byte* key, out Byml value);
                return new(Utf8StringMarshaller.ConvertToManaged(key)!, value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(BymlHash hash)
        {
            _hash = hash;
            _iterator = IntPtr.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return BymlHashNative.BymlHashIteratorAdvance(_hash, _iterator, out _iterator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _iterator = IntPtr.Zero;
        }

        public void Dispose() { }
    }

    protected override bool ReleaseHandle()
    {
        return IsClosed || _isChildNode || BymlHashNative.BymlHashFree(this);
    }
}
