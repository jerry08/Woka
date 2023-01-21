using AndroidX.AppCompat.Widget;
using Woka.Helpers.Keyboard;

namespace Woka.Handlers;

//https://github.com/dotnet/maui/issues/5983
public class EntryHandler : Microsoft.Maui.Handlers.EntryHandler
{
    protected override void ConnectHandler(AppCompatEditText platformView)
    {
        base.ConnectHandler(platformView);
        platformView.FocusChange += PlatformView_FocusChange;
    }

    protected override void DisconnectHandler(AppCompatEditText platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.FocusChange -= PlatformView_FocusChange;
    }

    private void PlatformView_FocusChange(object? sender, Android.Views.View.FocusChangeEventArgs args)
    {
        if (args.HasFocus)
            VirtualView.ShowKeyboard();
        else
            VirtualView.HideKeyboard();
    }
}