using Microsoft.Maui.Platform;

namespace Woka.Handlers;

public class RefreshViewHandler : Microsoft.Maui.Handlers.RefreshViewHandler
{
    protected override MauiRefreshView CreatePlatformView()
    {
        return new MauiRefreshView();
    }
}
