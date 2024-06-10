using Microsoft.Maui.Networking;
#if __IOS__ || MACCATALYST
using PlatformView = UIKit.UIImageView;
#elif ANDROID
using PlatformView = Android.Widget.ImageView;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.Image;
#elif TIZEN
using PlatformView = Tizen.UIExtensions.NUI.Image;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID && !TIZEN)
using PlatformView = System.Object;
#endif

namespace Woka.Handlers;

public class ImageHandler : Microsoft.Maui.Handlers.ImageHandler
{
    protected override void ConnectHandler(PlatformView platformView)
    {
        base.ConnectHandler(platformView);
        Connectivity.Current.ConnectivityChanged += ConnectivityChanged;
    }

    protected override void DisconnectHandler(PlatformView platformView)
    {
        base.DisconnectHandler(platformView);
        Connectivity.Current.ConnectivityChanged -= ConnectivityChanged;
    }

    private void ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess == NetworkAccess.Internet)
            SourceLoader.UpdateImageSourceAsync();
    }
}
