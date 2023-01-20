using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

#if IOS || MACCATALYST
using PlatformImage = UIKit.UIImage;
using PlatformView = UIKit.UIView;
#elif ANDROID
using PlatformImage = Android.Graphics.Drawables.Drawable;
using PlatformView = Android.Views.View;
#elif WINDOWS
using PlatformImage = Microsoft.UI.Xaml.Media.ImageSource;
using PlatformView = Microsoft.UI.Xaml.FrameworkElement;
#elif TIZEN
using PlatformImage = Microsoft.Maui.Platform.MauiImageSource;
using PlatformView = Tizen.NUI.BaseComponents.View;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformImage = System.Object;
using PlatformView = System.Object;
#endif

namespace Woka.Utils.Extensions;

public static class ImageSourcePartLoaderEx
{
    public static void SetNull(this ImageSourcePartLoader sourceLoader)
    {
        var type = sourceLoader.GetType();

        var p = type.GetProperty("SetImage", BindingFlags.NonPublic | BindingFlags.Instance);
        var test1 = (Action<PlatformImage?>?)p?.GetValue(sourceLoader);

        var sourceManager = type.GetProperty("SourceManager", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(sourceLoader);
        var test2 = sourceManager?.GetType()
            .GetMethod("CompleteLoad", new[] { typeof(IDisposable) });

        test1?.Invoke(null);
        test2?.Invoke(sourceManager, new object?[] { null });

        return;

        var method = type.GetMethod("SetImage");
        var method2 = type.GetMethod("SetImage", BindingFlags.NonPublic);
        var method3 = type.GetMethods().ToList();

        var test = method3.Where(x => x.Name.Contains("SetImage")).ToList();

        var props = type.GetProperties().Select(x => x.Name).ToList();
        var props2 = type.GetProperties(BindingFlags.NonPublic).Select(x => x.Name).ToList();
        var props3 = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList();

        var ff = type.GetFields().Select(x => x.Name).ToList();
        var ff2 = type.GetFields(BindingFlags.NonPublic).Select(x => x.Name).ToList();
        var ff3 = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList();
    }

    public static void Test(this ImageSourcePartLoader sourceLoader)
    {
        var sourceLoaderType = sourceLoader.GetType();

        var p = sourceLoaderType.GetProperty("SetImage", BindingFlags.NonPublic | BindingFlags.Instance);
        var test1 = (Action<PlatformImage?>?)p?.GetValue(sourceLoader);

        var sourceManager = sourceLoaderType.GetProperty("SourceManager", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.GetValue(sourceLoader);
        var completeLoadM = sourceManager?.GetType()
            .GetMethod("CompleteLoad", new[] { typeof(IDisposable) });

        test1?.Invoke(null);
        completeLoadM?.Invoke(sourceManager, new object?[] { null });

        return;

        var method = type.GetMethod("SetImage");
        var method2 = type.GetMethod("SetImage", BindingFlags.NonPublic);
        var method3 = type.GetMethods().ToList();

        var test = method3.Where(x => x.Name.Contains("SetImage")).ToList();

        var props = type.GetProperties().Select(x => x.Name).ToList();
        var props2 = type.GetProperties(BindingFlags.NonPublic).Select(x => x.Name).ToList();
        var props3 = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList();

        var ff = type.GetFields().Select(x => x.Name).ToList();
        var ff2 = type.GetFields(BindingFlags.NonPublic).Select(x => x.Name).ToList();
        var ff3 = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Select(x => x.Name).ToList();
    }
}