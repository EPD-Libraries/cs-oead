namespace CsOead;

public unsafe class BymlHash : SafeHandleZeroOrMinusOneIsInvalid // , IEnumerable<KeyValuePair<string, Byml>>, IEnumerable
{
    public BymlHash() : base(true) { }

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

    protected override bool ReleaseHandle()
    {
        return BymlHashNative.BymlHashFree(this);
    }
}
