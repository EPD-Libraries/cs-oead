namespace CsOead.Native;

internal static unsafe partial class Yaz0Native
{
    [LibraryImport("cs_oead")]
    internal static partial DataMarshal Compress(byte* src, int src_len, uint alignment, int level);

    [LibraryImport("cs_oead")]
    internal static partial void Decompress(byte* src, int src_len, byte* dst, int dst_len);
}
