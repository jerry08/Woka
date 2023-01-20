using System;
using System.Reflection;
using Microsoft.Maui.Platform;

#if IOS || MACCATALYST
using PlatformImage = UIKit.UIImage;
#elif ANDROID
using PlatformImage = Android.Graphics.Drawables.Drawable;
#elif WINDOWS
using PlatformImage = Microsoft.UI.Xaml.Media.ImageSource;
#elif TIZEN
using PlatformImage = Microsoft.Maui.Platform.MauiImageSource;
#elif NETSTANDARD || !PLATFORM || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformImage = System.Object;
#endif

namespace Woka.Utils.Extensions;

public static class ImageSourcePartLoaderEx
{
    public static void SetNullImage(this ImageSourcePartLoader sourceLoader)
    {
        var sourceLoaderType = sourceLoader.GetType();

        var setImageProp = sourceLoaderType.GetProperty("SetImage",
            BindingFlags.NonPublic | BindingFlags.Instance);
        var setImageM = (Action<PlatformImage?>?)setImageProp?.GetValue(sourceLoader);

        var sourceManager = sourceLoaderType.GetProperty("SourceManager",
            BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(sourceLoader);
        var completeLoadM = sourceManager?.GetType()
            .GetMethod("CompleteLoad", new[] { typeof(IDisposable) });

        setImageM?.Invoke(null);
        completeLoadM?.Invoke(sourceManager, new object?[] { null });
    }
}