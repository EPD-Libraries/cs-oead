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

/// <summary>
/// Instance wrapper around the <a href="https://oead.readthedocs.io/en/stable/sarc.html"><c>oead::Sarc</c></a> class<br/>
/// <b>Note:</b> This API does not match the c++ documentation entirely, the SarcWriter is now embedded into the <see cref="Sarc"/> wrapper
/// </summary>
public unsafe class Sarc : SafeHandleMinusOneIsInvalid, IEnumerable<KeyValuePair<string, DataMarshal>>, IEnumerable
{
    private Sarc() : base(true) { }
    public Sarc(Endianness endian = Endianness.Little, Mode mode = Mode.New) : base(true)
    {
        _writer = SarcNative.SarcWriterNew(endian, mode);
    }

    private IntPtr _writer = -1;
    private IntPtr Writer {
        get => _writer <= -1 ? _writer = SarcNative.SarcWriterFromSarc(this) : _writer;
            }

    private bool IsWriter => _writer > -1;

    /// <summary>
    /// Gets or sets the file matching the provided <paramref name="key"/>
    /// </summary>
    /// <exception cref="KeyNotFoundException"/>
    public ReadOnlySpan<byte> this[string key] {
        get => GetFile(key);
        set => Add(key, value);
    }

    /// <summary>
    /// Loads the input file or buffer into an unmanaged <a href="https://oead.readthedocs.io/en/stable/sarc.html#_CPPv4N4oead4SarcE">
    /// <c>oead::Sarc*</c></a> instance and returns the managed wrapper
    /// </summary>
    public static Sarc FromBinary(string inputFile) => FromBinary(File.ReadAllBytes(inputFile));

    /// <inheritdoc cref="FromBinary(string)"/>
    public static Sarc FromBinary(ReadOnlySpan<byte> data)
    {
        fixed (byte* ptr = data) {
            ResultMarshal result = SarcNative.SarcFromBinary(ptr, data.Length, out Sarc output);
            return result == Result.Ok ? output : throw new InvalidSarcException(result);
        }
    }

    /// <summary>
    /// Writes the underlying <a href="https://oead.readthedocs.io/en/stable/sarc.html#_CPPv4N4oead10SarcWriterE">
    /// <c>oead::SarcWriter*</c></a> and returns a <see cref="DataMarshal"/> of the
    /// unmanaged <a href="https://en.cppreference.com/w/cpp/container/vector"><c>std::vector&lt;u8&gt;</c></a>
    /// </summary>
    public DataMarshal ToBinary(Endianness? endianness = null, Mode? mode = null) => ToBinary(out _, endianness, mode);

    /// <inheritdoc cref="ToBinary(Endianness?, Mode?)"/>
    public DataMarshal ToBinary(out uint alignment, Endianness? endianness = null, Mode? mode = null)
    {
        if (endianness is Endianness _endianness) {
            SarcNative.SarcWriterSetEndianness(Writer, _endianness);
        }

        if (mode is Mode _mode) {
            SarcNative.SarcWriterSetMode(Writer, _mode);
        }

        ResultMarshal result = SarcNative.SarcToBinary(Writer, out alignment, out DataMarshal output);
        return result == Result.Ok ? output : throw new InvalidSarcException(result);
    }

    public int Count {
        get => IsWriter ? SarcNative.SarcWriterGetNumFiles(Writer) : SarcNative.SarcGetNumFiles(this);
    }

    public Endianness Endianness {
        get => SarcNative.SarcGetEndianness(this);
        set => SarcNative.SarcWriterSetEndianness(Writer, value);
    }

    /// <summary>
    /// Returns a span of data matching the provided <paramref name="key"/><br/>
    /// <b>Note:</b> the life of the returned <see cref="Span{T}"/> lasts only
    /// as long as the parent <see cref="Sarc"/>
    /// </summary>
    /// <exception cref="KeyNotFoundException"></exception>
    public Span<byte> GetFile(string key)
    {
        if (TryGetFile(key, out Span<byte> value)) {
            return value;
        }

        throw new KeyNotFoundException(
            $"The key '{key}' was not found in the archive");
    }

    /// <summary>
    /// Looks for the provided <paramref name="key"/> and sets the output
    /// <paramref name="value"/> if it exists in the archive (otherwise <see langword="null"/>)
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns><see langword="true"/> if the key exists in the archive, otherwise <see langword="false"/></returns>
    public bool TryGetFile(string key, out Span<byte> value)
    {
        bool result = IsWriter ? SarcNative.SarcWriterGetFile(Writer, key, out byte* dst, out int dst_len)
            : SarcNative.SarcGetFile(this, key, out dst, out dst_len);

        value = result ? new(dst, dst_len) : null;
        return result;
    }

    /// <summary>
    /// Checks if the provided <paramref name="key"/> exists in the archive
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ContainsKey(string key)
    {
        return SarcNative.SarcWriterContainsKey(Writer, key);
    }

    /// <summary>
    /// Adds a new file to the archive or sets the value if it already exists
    /// </summary>
    public void Add(string key, ReadOnlySpan<byte> value)
    {
        fixed (byte* ptr = value) {
            SarcNative.SarcWriterAddFile(Writer, key, ptr, value.Length);
        }
    }

    /// <summary>
    /// Removes the provided <paramref name="key"/> from the archive
    /// </summary>
    public void Remove(string key)
    {
        SarcNative.SarcWriterRemoveFile(Writer, key);
    }

    /// <summary>
    /// Removes every file from the archive
    /// </summary>
    public void Clear()
    {
        SarcNative.SarcWriterClearFiles(Writer);
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

    protected override bool ReleaseHandle()
    {
        return IsClosed || SarcNative.SarcFree(this, _writer);
    }
}
