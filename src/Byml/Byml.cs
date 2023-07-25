namespace CsOead;

/// <summary>
/// Instance wrapper around the <a href="https://oead.readthedocs.io/en/stable/byml.html"><c>oead::Byml</c></a> class
/// </summary>
public unsafe class Byml : SafeHandleZeroOrMinusOneIsInvalid
{
    private bool _isChildNode = true;

    private Byml() : base(true) { }
    private Byml(Byml _ref) : this()
    {
        handle = _ref.handle;
        _isChildNode = false;
    }

    public BymlType Type => BymlNative.BymlGetType(this);

    public static Byml FromBinary(string inputFile) => FromBinary(File.ReadAllBytes(inputFile));
    public static Byml FromBinary(ReadOnlySpan<byte> data)
    {
        fixed (byte* ptr = data) {
            ResultMarshal result = BymlNative.BymlFromBinary(ptr, data.Length, out Byml output);
            output._isChildNode = false;
            return result == Result.Ok ? output : throw new InvalidBymlException(result);
        }
    }

    public static Byml FromText(string text)
    {
        ResultMarshal result = BymlNative.BymlFromText(text, out Byml output);
        output._isChildNode = false;
        return result == Result.Ok ? output : throw new InvalidBymlException(result);
    }

    public DataMarshal ToBinary(Endianness? endianness = null, int version = 2)
    {
        ResultMarshal result = BymlNative.BymlToBinary(this, endianness == Endianness.Big, version, out DataMarshal output);
        return result == Result.Ok ? output : throw new InvalidBymlException(result);
    }

    public StringMarshal ToText()
    {
        ResultMarshal result = BymlNative.BymlToText(this, out StringMarshal output);
        return result == Result.Ok ? output : throw new InvalidBymlException(result);
    }

    public BymlHash GetHash()
        => BymlNative.BymlGetHash(this);

    public BymlArray GetArray()
        => BymlNative.BymlGetArray(this);

    public StringMarshal GetString()
        => BymlNative.BymlGetString(this);

    public DataMarshal GetBinary()
        => BymlNative.BymlGetBinary(this);

    public bool GetBool()
        => BymlNative.BymlGetBool(this);

    public int GetInt()
        => BymlNative.BymlGetInt(this);

    public uint GetUInt()
        => BymlNative.BymlGetUInt(this);

    public float GetFloat()
        => BymlNative.BymlGetFloat(this);

    public long GetInt64()
        => BymlNative.BymlGetInt64(this);

    public ulong GetUInt64()
        => BymlNative.BymlGetUInt64(this);

    public double GetDouble()
        => BymlNative.BymlGetDouble(this);

    public Byml(BymlHash value) : this((Byml)value) { }
    public static implicit operator Byml(BymlHash value)
        => BymlNative.BymlSetHash(value);

    public Byml(BymlArray value) : this((Byml)value) { }
    public static implicit operator Byml(BymlArray value)
        => BymlNative.BymlSetArray(value);

    public Byml(string value) : this((Byml)value) { }
    public static implicit operator Byml(string value)
        => BymlNative.BymlSetString(value);

    public Byml(byte[] value) : this((Byml)value) { }
    public static implicit operator Byml(byte[] value)
        => BymlSetBinary(value);

    public Byml(bool value) : this((Byml)value) { }
    public static implicit operator Byml(bool value)
        => BymlNative.BymlSetBool(value);

    public Byml(int value) : this((Byml)value) { }
    public static implicit operator Byml(int value)
        => BymlNative.BymlSetInt(value);

    public Byml(uint value) : this((Byml)value) { }
    public static implicit operator Byml(uint value)
        => BymlNative.BymlSetUInt(value);

    public Byml(float value) : this((Byml)value) { }
    public static implicit operator Byml(float value)
        => BymlNative.BymlSetFloat(value);

    public Byml(long value) : this((Byml)value) { }
    public static implicit operator Byml(long value)
        => BymlNative.BymlSetInt64(value);

    public Byml(ulong value) : this((Byml)value) { }
    public static implicit operator Byml(ulong value)
        => BymlNative.BymlSetUInt64(value);

    public Byml(double value) : this((Byml)value) { }
    public static implicit operator Byml(double value)
        => BymlNative.BymlSetDouble(value);

    private static Byml BymlSetBinary(byte[] value)
    {
        fixed (byte* ptr = value) {
            return BymlNative.BymlSetBinary(ptr, value.Length);
        }
    }

    protected override bool ReleaseHandle()
    {
        return IsClosed || _isChildNode || BymlNative.BymlFree(this);
    }
}
