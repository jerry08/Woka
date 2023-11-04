using CoreGraphics;
using Microsoft.Maui.Platform;
using UIKit;

namespace Woka.Handlers.RefreshView;

public class WokaRefreshView : MauiRefreshView
{
    UIView? _contentView;

    public override void AddSubview(UIView view)
    {
        base.AddSubview(view);
        _contentView = view;
    }

    public override CGRect Bounds
    {
        get => base.Bounds;
        set
        {
            base.Bounds = value;
            if (_contentView != null)
                _contentView.Frame = value;
        }
    }
}