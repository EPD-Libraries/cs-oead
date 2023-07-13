using CsOead.Exceptions;
using CsOead.Native;
using CsOead.Tests.Helpers;
using System.Text;

namespace CsOead.Tests;

[TestClass]
public class BymlTest
{
    static BymlTest() => OeadContext.Init();

    [TestMethod]
    public Task FromBinary()
    {
        byte[] raw = Resource.Input();
        Byml byml = Byml.FromBinary(raw);

        Assert.IsTrue(byml.Type == BymlType.Hash);

        return Task.CompletedTask;
    }

    [TestMethod]
    public Task FromText()
    {
        byte[] raw = Resource.Input();
        Byml byml = Byml.FromText(Encoding.UTF8.GetString(raw));

        Assert.IsTrue(byml.Type == BymlType.Hash);

        return Task.CompletedTask;
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidBymlException))]
    public Task InvalidFromBinary()
    {
        byte[] invalid = Resource.Input("Invalid");
        Byml.FromBinary(invalid);

        return Task.CompletedTask;
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidBymlException))]
    public Task InvalidFromText()
    {
        byte[] invalid = Resource.Input("Invalid");
        Byml.FromText(Encoding.UTF8.GetString(invalid));

        return Task.CompletedTask;
    }

    [TestMethod]
    public Task ToBinary()
    {
        byte[] raw = Resource.Input("FromBinary");
        Span<byte> buffer = Byml.FromBinary(raw).ToBinary();

        Byml byml = Byml.FromBinary(buffer);

        Assert.IsTrue(byml.Type == BymlType.Hash);

        return Task.CompletedTask;
    }

    [TestMethod]
    public Task ToText()
    {
        byte[] raw = Resource.Input("FromBinary");
        string text = Byml.FromBinary(raw).ToText().ToString();


        Byml byml = Byml.FromText(text);

        Assert.IsTrue(byml.Type == BymlType.Hash);

        return Task.CompletedTask;
    }
}
