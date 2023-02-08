using System.Drawing;

namespace Borowik.Gui.Wasm;

public static class ColorExtensions
{
    public static string ToHex(this Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    public static Color Invert(this Color color, bool invertAlpha = false)
    {
        return Color.FromArgb(invertAlpha ? 255 - color.A : color.A, 255 - color.R, 255 - color.G, 255 - color.B);
    }
}