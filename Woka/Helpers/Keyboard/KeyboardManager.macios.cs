using Microsoft.Maui.ApplicationModel;
using System;
using UIKit;

namespace Woka.Helpers.Keyboard;

public static partial class KeyboardManager
{
    public static void HideKeyboard()
    {
        Platform.GetCurrentUIViewController()?.ResignFirstResponder();
    }

    public static void HideKeyboard(this UIView inputView) => inputView.ResignFirstResponder();

    public static void ShowKeyboard(this UIView inputView) => inputView.BecomeFirstResponder();

    public static bool IsSoftKeyboardVisible(this UIView inputView) => inputView.IsFirstResponder;
}
