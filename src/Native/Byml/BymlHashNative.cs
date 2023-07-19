namespace CsOead.Native;

internal static unsafe partial class BymlHashNative
{
    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool BymlHashGet(BymlHash hash, string key, out Byml output);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void BymlHashAdd(BymlHash hash, string key, Byml value);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void BymlHashRemove(BymlHash hash, string key);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool BymlHashContainsKey(BymlHash hash, string key);

    [LibraryImport("cs_oead")]
    internal static partial void BymlHashClear(BymlHash hash);

    [LibraryImport("cs_oead")]
    internal static partial int BymlHashCount(BymlHash hash);

    [LibraryImport("cs_oead")]
    internal static partial void BymlHashIteratorCurrent(IntPtr iterator, out byte* key, out Byml value);

    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool BymlHashIteratorAdvance(BymlHash hash, IntPtr iterator, out IntPtr next);

    [LibraryImport("cs_oead")]
    internal static partial IntPtr BymlHashNew();

    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool BymlHashFree(BymlHash hash);
}
