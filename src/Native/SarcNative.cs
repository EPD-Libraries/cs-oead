namespace CsOead.Native;

/// <summary>
/// Internal function imports for <see cref="Sarc"/>
/// </summary>
internal static unsafe partial class SarcNative
{
    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SarcFromBinary(byte* src, int src_len, out Sarc output);

    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SarcToBinary(IntPtr writer, out DataMarshal output);

    [LibraryImport("cs_oead")]
    internal static partial int SarcGetNumFiles(Sarc sarc);

    [LibraryImport("cs_oead")]
    internal static partial Endianness SarcGetEndianness(Sarc sarc);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SarcGetFile(Sarc sarc, string name, out byte* dst, out int dst_len);

    [LibraryImport("cs_oead")]
    internal static partial int SarcWriterGetNumFiles(IntPtr writer);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SarcWriterGetFile(IntPtr writer, string name, out byte* dst, out int dst_len);

    [LibraryImport("cs_oead")]
    internal static partial IntPtr SarcWriterNew(Endianness endian, Mode mode);

    [LibraryImport("cs_oead")]
    internal static partial IntPtr SarcWriterFromSarc(Sarc sarc);

    [LibraryImport("cs_oead")]
    internal static partial void SarcWriterSetEndianness(IntPtr writer, Endianness endianness);

    [LibraryImport("cs_oead")]
    internal static partial void SarcWriterSetMode(IntPtr writer, Mode mode);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SarcWriterContainsKey(IntPtr writer, string name);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void SarcWriterAddFile(IntPtr writer, string name, byte* src, int src_len);

    [LibraryImport("cs_oead", StringMarshalling = StringMarshalling.Utf8)]
    internal static partial void SarcWriterRemoveFile(IntPtr writer, string name);

    [LibraryImport("cs_oead")]
    internal static partial void SarcWriterClearFiles(IntPtr writer);

    [LibraryImport("cs_oead")]
    internal static partial void SarcIteratorCurrent(IntPtr iterator, out byte* key, out DataMarshal value);

    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SarcIteratorAdvance(IntPtr hash, IntPtr iterator, out IntPtr next);

    [LibraryImport("cs_oead")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SarcFree(Sarc sarc, IntPtr writer);
}
