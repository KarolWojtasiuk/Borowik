using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;

namespace Borowik.Gui.Services;

internal static class StorageProviderLocator
{
    private static IStorageProvider? _storageProvider;

    public static void SetStorageProvider(Visual visual)
    {
        _storageProvider = (visual.GetVisualRoot() as TopLevel)?.StorageProvider;
    }

    public static IStorageProvider? GetStorageProvider()
    {
        return _storageProvider;
    }
}