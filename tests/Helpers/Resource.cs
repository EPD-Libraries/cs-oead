using System.Runtime.CompilerServices;

namespace CsOead.Tests.Helpers;

public static class Resource
{
    public static byte[] Input([CallerMemberName] string method = "", [CallerFilePath] string file = "")
    {
        return File.ReadAllBytes(
            Path.Combine(
                AppContext.BaseDirectory, "Data", Path.GetFileNameWithoutExtension(file), $"{method}.res")
        );
    }
}
