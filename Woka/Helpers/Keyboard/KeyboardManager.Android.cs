using System;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.Core.View;
using Google.Android.Material.TextField;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace Woka.Helpers.Keyboard;

public static partial class KeyboardManager
{
    static void ShowKeyboard(this AView inputView)
    {
        (inputView.GetInputView() as TextView)?.ShowKeyboard();
    }

    public static void HideKeyboard()
    {
        if (Platform.CurrentActivity?.CurrentFocus is not null)
            Platform.CurrentActivity?.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
    }

    static void HideKeyboard(this AView inputView, bool overrideValidation = false)
    {
        inputView = inputView.GetInputView();

        if (inputView?.Context == null)
            throw new ArgumentNullException(
                nameof(inputView) + " must be set before the keyboard can be hidden."
            );

        using var inputMethodManager = (InputMethodManager)
            inputView.Context.GetSystemService(Context.InputMethodService)!;
        if (
            !overrideValidation
            && !(inputView is EditText || inputView is TextView || inputView is SearchView)
        )
            throw new ArgumentException(
                "inputView should be of type EditText, SearchView, or TextView"
            );

        var windowToken = inputView.WindowToken;
        if (windowToken != null && inputMethodManager != null)
            inputMethodManager.HideSoftInputFromWindow(windowToken, HideSoftInputFlags.None);
    }

    static void ShowKeyboard(this TextView inputView, bool focusRequested = false)
    {
        if (inputView?.Context == null)
            throw new ArgumentNullException(
                nameof(inputView) + " must be set before the keyboard can be shown."
            );

        using var inputMethodManager = (InputMethodManager)
            inputView.Context.GetSystemService(Context.InputMethodService)!;
        if (!inputView.HasFocus)
        {
            // The keyboard only likes to open when you've focused the element that the keyboard will interact with
            // so we request focus and then call showkeyboard a second time
            inputView.RequestFocus();
            new Handler(Looper.MainLooper).Post(() => inputView.ShowKeyboard(true));
        }
        else
        {
            inputMethodManager?.ShowSoftInput(inputView, 0);
        }
    }

    static AView GetInputView(this AView view)
    {
        return view switch
        {
            SearchView searchView => searchView.FindViewById(
                searchView.Resources.GetIdentifier("android:id/search_src_text", null, null)
            ),
            TextView textView => textView,
            TextInputLayout inputLayout => inputLayout.EditText,
            _ => throw new Exception($"Unable to locate `TextView` for {view}"),
        };
    }

    static bool IsSoftKeyboardVisible(this AView view)
    {
        view = view.GetInputView();

        var insets = ViewCompat.GetRootWindowInsets(view);
        if (insets == null)
            return false;

        var result = insets.IsVisible(WindowInsetsCompat.Type.Ime());
        return result;
    }
}
