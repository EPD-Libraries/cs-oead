using System.Buffers.Binary;

namespace CsOead;

/// <summary>
/// Static, functional wrapper around the <a href="https://oead.readthedocs.io/en/stable/yaz0.html"><c>oead::yaz0</c></a> namespace
/// </summary>
public unsafe class Yaz0
{
    /// <summary>
    /// Compresses the target file or buffer with Yaz0 and returns a <see cref="DataMarshal"/> of the unmanaged
    /// <a href="https://en.cppreference.com/w/cpp/container/vector"><c>std::vector&lt;u8&gt;</c></a>
    /// </summary>
    /// <param name="alignment">The file alignment (Default: 0)</param>
    /// <param name="level">Compression level, 1-9 is valid (Default: 7)</param>
    public static DataMarshal Compress(string inputFile, uint alignment = 0, int level = 7) => Compress(File.ReadAllBytes(inputFile), alignment, level);

    /// <inheritdoc cref="Compress(string, uint, int)"/>
    public static DataMarshal Compress(ReadOnlySpan<byte> src, uint alignment = 0, int level = 7)
    {
        fixed (byte* ptr = src) {
            return Yaz0Native.Compress(ptr, src.Length, alignment, level);
        }
    }

    /// <summary>
    /// Checks the input file or buffer for the Yaz0 header and returns the decompressed <paramref name="buffer"/> if the header was found (otherwise <see langword="null"/>)
    /// </summary>
    /// <param name="buffer">The decompressed buffer</param>
    public static bool TryDecompress(string inputFile, out byte[]? buffer) => TryDecompress(File.ReadAllBytes(inputFile), out buffer);

    /// <inheritdoc cref="TryDecompress(string, out byte[]?)"/>
    public static bool TryDecompress(ReadOnlySpan<byte> src, out byte[]? buffer)
    {
        bool result = src.Length > 4 && src[..4].SequenceEqual("Yaz0"u8);
        buffer = result ? Decompress(src) : null;
        return result;
    }

    /// <summary>
    /// Decompresses the input file or buffer and returns the result as a managed <see langword="byte"/>[]
    /// </summary>
    public static byte[] Decompress(string inputFile) => Decompress(File.ReadAllBytes(inputFile));

    /// <inheritdoc cref="Decompress(string)"/>
    public static byte[] Decompress(ReadOnlySpan<byte> src)
    {
        int decompressedSize = BinaryPrimitives.ReadInt32BigEndian(src[0x04..0x08]);
        byte[] dst = new byte[decompressedSize];

        fixed (byte* ptr = src) {
            fixed (byte* dst_ptr = dst) {
                Yaz0Native.Decompress(ptr, src.Length, dst_ptr, decompressedSize);
            }
        }

        return dst;
    }
}
