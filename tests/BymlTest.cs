using CsOead.Exceptions;
using CsOead.Native;
using CsOead.Tests.Helpers;
using System;
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
    public Task General()
    {
        byte[] raw = Resource.Input("FromBinary");
        Byml byml = Byml.FromBinary(raw);

        Assert.IsTrue(byml.Type == BymlType.Hash);

        BymlHash root = byml.GetHash();

        BymlHash bymlHash = root["byml_hash"].GetHash();
        foreach ((var key, var value) in bymlHash) {
            Assert.IsTrue(key switch {
                "string" => value.Type == BymlType.String
                    && value.GetString().ToString() == "A1B2C3",
                "binary" => value.Type == BymlType.Binary
                    && value.GetBinary().AsSpan().SequenceEqual("A1B2C3"u8),
                "bool" => value.Type == BymlType.Bool
                    && value.GetBool() == true,
                "int" => value.Type == BymlType.Int
                    && value.GetInt() == 0,
                "uint" => value.Type == BymlType.UInt
                    && value.GetUInt() == 0x0U,
                "float" => value.Type == BymlType.Float
                    && value.GetFloat() == 0.0,
                "int64" => value.Type == BymlType.Int64
                    && value.GetInt64() == 0x0L,
                "uint64" => value.Type == BymlType.UInt64
                    && value.GetUInt64() == 0x0UL,
                "double" => value.Type == BymlType.Double
                    && value.GetDouble() == 0.0D,
                _ => false
            });
        }

        bymlHash.Remove("string");

        Assert.IsFalse(bymlHash.TryGet("string", out _));
        Assert.IsFalse(bymlHash.ContainsKey("string"));
        Assert.ThrowsException<KeyNotFoundException>(() => bymlHash["string"]);

        bymlHash["string"] = "A1B2C3";
        Assert.IsTrue(bymlHash["string"].GetString().ToString() == "A1B2C3");

        bymlHash.Clear();
        Assert.IsFalse(bymlHash.Count > 0);

        BymlHash bymlArray = root["byml_hash"].GetHash();

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
