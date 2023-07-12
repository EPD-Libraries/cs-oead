using Native.IO.Services;

namespace CsOead.Tests.Helpers;

public static class OeadContext
{
    private static bool _isLoaded = false;

    public static void Init()
    {
        if (!_isLoaded) {
            NativeLibraryManager
                .RegisterPath("lib", out bool isCommonLoaded)
                .Register(new OeadLibrary(), out bool isOeadLoaded);

            Console.WriteLine($"""
                Common Library: {isCommonLoaded}
                Oead Library: {isOeadLoaded}
                """);

            _isLoaded = true;
        }
    }
}
