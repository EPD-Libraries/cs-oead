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

    public static string Output([CallerMemberName] string method = "", [CallerFilePath] string file = "")
    {
        return Path.Combine(
            AppContext.BaseDirectory, "Data", Path.GetFileNameWithoutExtension(file), $"{method}.output.res");
    }
}
