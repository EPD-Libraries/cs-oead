using CsOead.Exceptions;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace CsOead;

public enum Endianness
{
    Big, Little
}

public enum Mode
{
    Legacy, New
}

public unsafe class Sarc : SafeHandleMinusOneIsInvalid, IEnumerable<KeyValuePair<string, DataMarshal>>, IEnumerable
{
    public Sarc() : base(true) { }

    public Sarc(Endianness endian = Endianness.Little, Mode mode = Mode.New) : base(true)
    {
        _writer = SarcNative.SarcWriterNew(endian, mode);
    }

    private IntPtr _writer = -1;
    private IntPtr Writer {
        get => _writer <= -1 ? _writer = SarcNative.SarcWriterFromSarc(this) : _writer;
    }

    private bool IsWriter => _writer > -1;

    public static Sarc FromBinary(string inputFile) => FromBinary(File.ReadAllBytes(inputFile));
    public static Sarc FromBinary(ReadOnlySpan<byte> data)
    {
        fixed (byte* ptr = data) {
            if (SarcNative.SarcFromBinary(ptr, data.Length, out Sarc output)) {
                return output;
            }
        }

        throw new InvalidSarcException(
            "The provided data could not be read");
    }

    public DataMarshal ToBinary(Endianness? endianness = null, Mode? mode = null)
    {
        if (endianness is Endianness _endianness) {
            SarcNative.SarcWriterSetEndianness(Writer, _endianness);
        }

        if (mode is Mode _mode) {
            SarcNative.SarcWriterSetMode(Writer, _mode);
        }

        if (SarcNative.SarcToBinary(Writer, out DataMarshal output)) {
            return output;
        }

        throw new InvalidSarcException(
            $"Could not serialize {(IsWriter ? "SarcWriter" : "Sarc")}");
    }

    public int Count {
        get => IsWriter ? SarcNative.SarcWriterGetNumFiles(Writer) : SarcNative.SarcGetNumFiles(this);
    }

    public Endianness Endianness {
        get => SarcNative.SarcGetEndianness(this);
        set => SarcNative.SarcWriterSetEndianness(Writer, value);
    }

    public Span<byte> GetFile(string key)
    {
        if (TryGetFile(key, out Span<byte> value)) {
            return value;
        }

        throw new KeyNotFoundException(
            $"The key '{key}' was not found in the archive");
    }

    public bool TryGetFile(string key, out Span<byte> value)
    {
        bool result = IsWriter ? SarcNative.SarcWriterGetFile(Writer, key, out byte* dst, out int dst_len)
            : SarcNative.SarcGetFile(this, key, out dst, out dst_len);

        value = result ? new(dst, dst_len) : null;
        return result;
    }

    public bool ContainsKey(string key)
    {
        return SarcNative.SarcWriterContainsKey(Writer, key);
    }

    public void Add(string key, ReadOnlySpan<byte> value)
    {
        fixed (byte* ptr = value) {
            SarcNative.SarcWriterAddFile(Writer, key, ptr, value.Length);
        }
    }

    public void Remove(string key)
    {
        SarcNative.SarcWriterRemoveFile(Writer, key);
    }

    public void Clear()
    {
        SarcNative.SarcWriterClearFiles(Writer);
    }

    protected override bool ReleaseHandle()
    {
        return SarcNative.SarcFree(this, _writer);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<KeyValuePair<string, DataMarshal>> GetEnumerator()
    {
        return new Enumerator(this);
    }

    public struct Enumerator : IEnumerator<KeyValuePair<string, DataMarshal>>, IEnumerator
    {
        private readonly Sarc _sarc;
        private IntPtr _iterator;

        object IEnumerator.Current => Current;
        public KeyValuePair<string, DataMarshal> Current {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                SarcNative.SarcIteratorCurrent(_iterator, out byte* key, out DataMarshal value);
                return new(Utf8StringMarshaller.ConvertToManaged(key)
                    ?? throw new NullReferenceException("The UTF8 converter returned null when parsing the unmanged string pointer"), value);;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(Sarc sarc)
        {
            _sarc = sarc;
            _iterator = IntPtr.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return SarcNative.SarcIteratorAdvance(_sarc.Writer, _iterator, out _iterator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            _iterator = IntPtr.Zero;
        }

        public void Dispose() { }
    }
}
