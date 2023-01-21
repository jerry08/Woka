using Microsoft.Maui.Platform;

namespace Woka.Handlers;

public class RefreshViewHandler : Microsoft.Maui.Handlers.RefreshViewHandler
{
    protected override void ConnectHandler(MauiSwipeRefreshLayout platformView)
    {
        //You can play around with the offsets
        platformView.SetProgressViewOffset(false, -120, 60);

        base.ConnectHandler(platformView);
    }
}