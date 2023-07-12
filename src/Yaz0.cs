using Native.IO.Handles;
using System.Buffers.Binary;
using System.Runtime.InteropServices;

namespace CsOead;

/// <summary>
/// Static, functional wrapper around the <a href="https://oead.readthedocs.io/en/stable/yaz0.html"><c>oead::yaz0</c></a> namespace
/// </summary>
public unsafe partial class Yaz0
{
    [LibraryImport("cs_oead")]
    private static partial DataMarshal Compress(byte* src, int src_len, uint alignment, int level);

    [LibraryImport("cs_oead")]
    private static partial void Decompress(byte* src, int src_len, byte* dst, int dst_len);

    public static DataMarshal Compress(string inputFile, uint alignment = 0, int level = 7) => Compress(File.ReadAllBytes(inputFile), alignment, level);
    public static DataMarshal Compress(ReadOnlySpan<byte> src, uint alignment = 0, int level = 7)
    {
        fixed (byte* ptr = src) {
            return Compress(ptr, src.Length, alignment, level);
        }
    }

    public static bool TryDecompress(string inputFile, out byte[]? buffer) => TryDecompress(File.ReadAllBytes(inputFile), out buffer);
    public static bool TryDecompress(ReadOnlySpan<byte> src, out byte[]? buffer)
    {
        bool result = src.Length > 4 && src[..4].SequenceEqual("Yaz0"u8);
        buffer = result ? Decompress(src) : null;
        return result;
    }

    public static byte[] Decompress(string inputFile) => Decompress(File.ReadAllBytes(inputFile));
    public static byte[] Decompress(ReadOnlySpan<byte> src)
    {
        int decompressedSize = BinaryPrimitives.ReadInt32BigEndian(src[0x04..0x08]);
        byte[] dst = new byte[decompressedSize];

        fixed (byte* ptr = src) {
            fixed (byte* dst_ptr = dst) {
                Decompress(ptr, src.Length, dst_ptr, decompressedSize);
            }
        }

        return dst;
    }
}
