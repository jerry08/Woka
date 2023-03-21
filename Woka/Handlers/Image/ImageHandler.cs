using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Networking;

namespace Woka.Handlers;

public class ImageHandler : Microsoft.Maui.Handlers.ImageHandler
{
    public Image? Image { get; set; }

    public override void SetVirtualView(IView view)
    {
        base.SetVirtualView(view);

        if (VirtualView is Image image)
        {
            Image = image;

            Connectivity.Current.ConnectivityChanged -= ConnectivityChanged;
            Connectivity.Current.ConnectivityChanged += ConnectivityChanged;
        }
    }

    private void ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess == NetworkAccess.Internet)
            SourceLoader.UpdateImageSourceAsync();
    }
}