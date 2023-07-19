namespace CsOead.Native;

internal static partial class BymlArrayNative
{
    [LibraryImport("cs_oead")]
    internal static partial Byml BymlArrayGet(BymlArray array, int index);

    [LibraryImport("cs_oead")]
    internal static partial void BymlArraySet(BymlArray array, int index, Byml value);

    [LibraryImport("cs_oead")]
    internal static partial void BymlArrayAdd(BymlArray array, Byml value);

    [LibraryImport("cs_oead")]
    internal static partial void BymlArrayRemove(BymlArray array, Byml value);

    [LibraryImport("cs_oead")]
    internal static partial void BymlArrayRemoveAt(BymlArray array, int index);

    [LibraryImport("cs_oead")]
    internal static partial void BymlArrayClear(BymlArray array);

    [LibraryImport("cs_oead")]
    internal static partial int BymlArrayLength(BymlArray array);

    [LibraryImport("cs_oead")]
    internal static partial IntPtr BymlArrayNew();

    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool BymlArrayFree(BymlArray array);
}
