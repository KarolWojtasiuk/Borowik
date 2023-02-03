using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.VisualTree;

namespace Borowik.Gui;

public static class VisualExtensions
{
    public static IStorageProvider? GetStorageProvider(this Visual visual) =>
        (visual.GetVisualRoot() as TopLevel)?.StorageProvider;
}