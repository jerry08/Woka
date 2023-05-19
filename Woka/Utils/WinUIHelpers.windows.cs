using WThickness = Microsoft.UI.Xaml.Thickness;

namespace Woka.Utils;

internal static class WinUIHelpers
{
    public static WThickness CreateThickness(double left, double top, double right, double bottom)
    {
        return new WThickness
        {
            Left = left,
            Top = top,
            Right = right,
            Bottom = bottom
        };
    }

    public static WThickness CreateThickness(double all)
    {
        return new WThickness
        {
            Left = all,
            Top = all,
            Right = all,
            Bottom = all
        };
    }
}