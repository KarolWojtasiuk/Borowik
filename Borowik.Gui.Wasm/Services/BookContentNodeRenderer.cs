using Blazorise;
using Borowik.Books.Entities;
using Microsoft.AspNetCore.Components;

namespace Borowik.Gui.Wasm.Services;

public static class BookContentNodeRenderer
{
    public static RenderFragment Render(IBookContentNode node) => builder =>
    {
        if (node is BookContentNodes.ParagraphNode paragraphNode)
        {
            builder.OpenComponent<Paragraph>(0);
            builder.AddAttribute(1, nameof(Text.ChildContent), (RenderFragment)(b => b.AddContent(2, paragraphNode.Value)));
            builder.CloseComponent();
        }

        if (node is BookContentNodes.ImageNode imageNode)
        {
            builder.OpenComponent<Image>(0);
            builder.AddAttribute(1, nameof(Image.Source), $"data:{imageNode.Value.MimeType};base64,{Convert.ToBase64String(imageNode.Value.Data)}");
            builder.CloseComponent();
        }
    };
}