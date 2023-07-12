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
    [ExpectedException(typeof(InvalidSarcException))]
    public Task Invalid()
    {
        byte[] invalid = Resource.Input();
        Sarc.FromBinary(invalid);

        return Task.CompletedTask;
    }
}
