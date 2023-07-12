namespace CsOead.Native;

internal static unsafe partial class Yaz0Native
{
    [LibraryImport("cs_oead")]
    internal static partial ResultMarshal Compress(byte* src, int src_len, uint alignment, int level, out DataMarshal output);

    [LibraryImport("cs_oead")]
    internal static partial ResultMarshal Decompress(byte* src, int src_len, byte* dst, int dst_len);
}
