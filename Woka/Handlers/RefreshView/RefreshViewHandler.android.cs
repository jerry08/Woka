using Android.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woka.Handlers;

public class RefreshViewHandler : Microsoft.Maui.Handlers.RefreshViewHandler
{
    protected override void ConnectHandler(MauiSwipeRefreshLayout platformView)
    {
        //You can play around with the offsets
        platformView.SetProgressViewOffset(false, -120, 60);

        //var f = PlatformView.Class.GetDeclaredField("mCircleView");
        //var f = PlatformView.Class.Superclass.GetDeclaredField("mCircleView");
        //f.Accessible = true;
        //var img = f.Get(PlatformView) as ImageView;

        base.ConnectHandler(platformView);
    }
}