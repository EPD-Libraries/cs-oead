namespace CsOead.Native;

public enum BymlType : int
{
    Null,
    String,
    Binary,
    Array,
    Hash,
    Bool,
    Int,
    Float,
    UInt,
    Int64,
    UInt64,
    Double
}

internal static unsafe partial class BymlNative
{
    [LibraryImport("cs_oead")]
    internal static partial ResultMarshal BymlFromBinary(byte* src, int src_len, out Byml output);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    internal static partial ResultMarshal BymlFromText(string src, out Byml output);

    [LibraryImport("cs_oead")]
    internal static partial ResultMarshal BymlToBinary(Byml byml, [MarshalAs(UnmanagedType.Bool)] bool big_endian, int version, out DataMarshal output);

    [LibraryImport("cs_oead")]
    internal static partial ResultMarshal BymlToText(Byml byml, out StringMarshal output);

    [LibraryImport("cs_oead")]
    internal static partial BymlType BymlGetType(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial BymlHash BymlGetHash(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial BymlArray BymlGetArray(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial StringMarshal BymlGetString(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial DataMarshal BymlGetBinary(Byml byml);

    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool BymlGetBool(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial int BymlGetInt(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial uint BymlGetUInt(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial float BymlGetFloat(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial long BymlGetInt64(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial ulong BymlGetUInt64(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial double BymlGetDouble(Byml byml);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetHash(BymlHash value);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetArray(BymlArray value);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    internal static partial Byml BymlSetString(string value);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetBinary(byte* value, int value_len);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetBool([MarshalAs(UnmanagedType.Bool)] bool value);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetInt(int value);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetUInt(uint value);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetFloat(float value);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetInt64(long value);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetUInt64(ulong value);

    [LibraryImport("cs_oead")]
    internal static partial Byml BymlSetDouble(double value);

    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool BymlFree(Byml byml);
}
