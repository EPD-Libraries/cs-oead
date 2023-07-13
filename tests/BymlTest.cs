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

        BymlArray bymlArray = root["byml_array"].GetArray();

        foreach (var item in bymlArray) {
            Assert.IsTrue(item.Type switch {
                BymlType.Int => item.GetInt() == 0,
                BymlType.Float => item.GetFloat() == 1.0,
                BymlType.UInt => item.GetUInt() == 0x02,
                BymlType.Int64 => item.GetInt64() == 3L,
                BymlType.String => item.GetString().ToString() == "4",
                _ => false
            });
        }

        Byml arbitrary = bymlArray[3];
        bymlArray.Remove(arbitrary);

        Assert.IsTrue(bymlArray.Length == 4);

        bymlArray.Add("string");

        Assert.IsTrue(bymlArray.Last().GetString().ToString() == "string");
        Assert.IsTrue(bymlArray.Length == 5);

        bymlArray.Clear();
        Assert.IsFalse(bymlArray.Length > 0);

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
