using Borowik.Books.Entities;

namespace Borowik.Gui.Wasm.Models;

public class EditBookModel
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public BookImage? Cover { get; set; }
}