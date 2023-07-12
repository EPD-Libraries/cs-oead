using Native.IO;

namespace CsOead;

public class OeadLibrary : NativeLibrary<OeadLibrary>
{
    protected override string Name { get; } = "cs_oead";
}
