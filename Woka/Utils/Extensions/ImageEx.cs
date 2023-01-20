using Microsoft.Maui.Controls;
using Microsoft.Maui.Handlers;

namespace Woka.Utils.Extensions;

public static class ImageEx
{
    public static void SetNullImage(this Image image)
    {
        if (image.Handler is ImageHandler handler)
            handler.SourceLoader.SetNullImage();
    }
}