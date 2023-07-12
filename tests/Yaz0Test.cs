using CsOead.Tests.Helpers;
using Native.IO.Handles;
using Native.IO.Services;
using System.Buffers.Binary;
using System.Text;

namespace CsOead.Tests;

[TestClass]
public class Yaz0Test
{
    static Yaz0Test()
    {
        NativeLibraryManager
            .RegisterPath("lib", out bool isCommonLoaded)
            .Register(new OeadLibrary(), out bool isOeadLoaded);

        Console.WriteLine($"""
            Common Library: {isCommonLoaded}
            Oead Library: {isOeadLoaded}
            """);
    }

    [TestMethod]
    public Task Compress()
    {
        byte[] raw = Resource.Input();
        using DataMarshal result = Yaz0.Compress(raw);
        Span<byte> span = result;

        Assert.IsTrue(span.Length >= 8);
        Assert.IsTrue(span[0..4].SequenceEqual("Yaz0"u8));
        Assert.IsTrue(BinaryPrimitives.ReadInt32BigEndian(span[4..8]) == raw.Length);

        return Task.CompletedTask;
    }

    [TestMethod]
    public Task Decompress()
    {
        byte[] compressed = Resource.Input();
        byte[] raw = Yaz0.Decompress(compressed);

        Assert.IsTrue(Encoding.UTF8.GetString(raw) == "Arbitrary decompressed data");
        Assert.IsTrue(BinaryPrimitives.ReadInt32BigEndian(compressed.AsSpan()[4..8]) == raw.Length);

        return Task.CompletedTask;
    }
}
