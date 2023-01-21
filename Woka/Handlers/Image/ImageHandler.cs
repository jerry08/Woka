using System;
using System.ComponentModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Networking;
using Woka.Utils.Extensions;

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

            Image.PropertyChanged -= PropertyChanged;
            Image.PropertyChanged += PropertyChanged;

            Image.BindingContextChanged -= BindingContextChanged;
            Image.BindingContextChanged += BindingContextChanged;
        }
    }

    private void BindingContextChanged(object? sender, EventArgs e)
        => ReloadImage();

    private void PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Image.Source) && Image?.Source is null)
            Image?.SetNullImage();
    }

    private void ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess == NetworkAccess.Internet)
            ReloadImage();
    }

    public void ReloadImage()
    {
        Image?.SetNullImage();
        SourceLoader.UpdateImageSourceAsync();
    }
}