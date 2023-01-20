using Android.Views.InputMethods;
using AndroidX.AppCompat.Widget;
using Woka.Helpers.Keyboard;
using Woka.Utils;

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
        /*var inputMethodManager = (InputMethodManager?)global::Android.App.Application.Context.GetSystemService(global::Android.Content.Context.InputMethodService);

        if (args.HasFocus)
            //inputMethodManager?.ShowSoftInput(PlatformView, ShowFlags.Forced);
            //inputMethodManager?.ShowSoftInput(PlatformView, ShowFlags.Implicit);
            inputMethodManager?.ShowSoftInput(PlatformView, 0);
        else
            inputMethodManager?.HideSoftInputFromWindow(PlatformView.WindowToken, HideSoftInputFlags.None);*/

        if (args.HasFocus)
            KeyboardManager.ShowKeyboard(VirtualView);
        else
            KeyboardManager.HideKeyboard(VirtualView);
    }
}