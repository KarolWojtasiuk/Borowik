using System.Drawing;
using Borowik.Books;
using Microsoft.AspNetCore.Components;

namespace Borowik.Gui.Wasm.Pages;

public partial class Index : ComponentBase
{
    [Inject] private IBookshelfManager BookshelfManager { get; init; } = null!;

    public async Task TestAsync()
    {
        var x = await BookshelfManager.CreateBookshelfAsync("Test", "desc", Color.Aqua, default);
    }
}