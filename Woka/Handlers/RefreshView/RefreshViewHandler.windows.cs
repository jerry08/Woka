using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace Woka.Handlers;

public class RefreshViewHandler : Microsoft.Maui.Handlers.RefreshViewHandler
{
    protected override void ConnectHandler(RefreshContainer nativeView)
    {
        if (PlatformView is not null)
        {
            PlatformView.ManipulationMode = ManipulationModes.All;
            PlatformView.ManipulationDelta += OnManipulationDelta;
        }

        base.ConnectHandler(nativeView);
    }

    protected override void DisconnectHandler(RefreshContainer nativeView)
    {
        if (PlatformView is not null)
        {
            PlatformView.ManipulationDelta -= OnManipulationDelta;
        }

        base.DisconnectHandler(nativeView);
    }

    void OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
        if (e.PointerDeviceType is PointerDeviceType.Touch)
            return; // Already managed by the RefreshContainer control itself

        const double minimumCumulativeY = 20;
        double cumulativeY = e.Cumulative.Translation.Y;

        if (cumulativeY > minimumCumulativeY && VirtualView?.IsRefreshing == false)
            VirtualView.IsRefreshing = true;
    }
}