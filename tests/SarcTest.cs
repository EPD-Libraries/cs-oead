using CsOead.Exceptions;
using CsOead.Tests.Helpers;
using System.Text;

namespace CsOead.Tests;

[TestClass]
public class SarcTest
{
    static SarcTest() => OeadContext.Init();

    [TestMethod]
    public Task FromBinary()
    {
        byte[] raw = Resource.Input();
        Sarc sarc = Sarc.FromBinary(raw);

        Assert.IsTrue(sarc.Endianness == Endianness.Little);
        Assert.IsTrue(sarc.Count == 15);

        return Task.CompletedTask;
    }

    [TestMethod]
    public Task ToBinary()
    {
        byte[] raw = Resource.Input("FromBinary");
        Span<byte> buffer = Sarc.FromBinary(raw).ToBinary(Endianness.Big);

        Assert.IsTrue(buffer[0..4].SequenceEqual("SARC"u8));

        Sarc sarc = Sarc.FromBinary(buffer);

        Assert.IsTrue(sarc.Endianness == Endianness.Big);
        Assert.IsTrue(sarc.Count == 15);

        return Task.CompletedTask;
    }

    [TestMethod]
    public Task Iterator()
    {
        byte[] raw = Resource.Input("FromBinary");
        Sarc sarc = Sarc.FromBinary(raw);

        foreach ((var file, var buffer) in sarc) {
            Console.WriteLine(file);
            string content = Path.GetFileNameWithoutExtension(file);
            Assert.IsTrue(Encoding.UTF8.GetString(buffer) == content);
        }

        return Task.CompletedTask;
    }

    [TestMethod]
    public Task General()
    {
        byte[] raw = Resource.Input("FromBinary");
        Sarc sarc = Sarc.FromBinary(raw);

        sarc["A/A1.res"] = "!A1"u8;
        Assert.IsTrue(sarc["A/A1.res"].SequenceEqual("!A1"u8));

        sarc.Add("D/D1.res", "D1"u8);
        sarc["D/D2.res"] = "D2"u8;

        Assert.IsTrue(sarc.Count == 17);
        Assert.IsTrue(sarc.ContainsKey("D/D1.res"));
        Assert.IsTrue(sarc.ContainsKey("D/D2.res"));

        Assert.IsTrue(sarc.GetFile("D/D1.res").SequenceEqual("D1"u8));
        Assert.IsTrue(sarc["D/D2.res"].SequenceEqual("D2"u8));

        sarc.Remove("D/D2.res");
        Assert.IsFalse(sarc.TryGetFile("D/D2.res", out _));

        sarc.Clear();
        Assert.IsFalse(sarc.Count > 0);

        return Task.CompletedTask;
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidSarcException))]
    public Task Invalid()
    {
        byte[] invalid = Resource.Input();
        Sarc.FromBinary(invalid);

        return Task.CompletedTask;
    }
}
