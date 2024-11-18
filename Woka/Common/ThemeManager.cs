using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace Woka;

public static class ThemeManager
{
    public static void Setup()
    {
        if (Application.Current is null)
            return;

        Application.Current.RequestedThemeChanged += (s, e) => Apply();
    }

    /// <summary>
    /// Applies theme
    /// </summary>
    public static void Apply()
    {
        if (Application.Current is null)
            return;

#if ANDROID
        //AppCompatDelegate.DefaultNightMode = (int)UiNightMode.Yes;
        AndroidX.AppCompat.App.AppCompatDelegate.DefaultNightMode = Application
            .Current
            .UserAppTheme switch
        {
            AppTheme.Light => AndroidX.AppCompat.App.AppCompatDelegate.ModeNightNo,
            AppTheme.Dark => AndroidX.AppCompat.App.AppCompatDelegate.ModeNightYes,
            _ => AndroidX.AppCompat.App.AppCompatDelegate.ModeNightFollowSystem,
        };
#elif IOS
        var viewController = Platform.GetCurrentUIViewController();
        if (viewController is null)
            return;

        viewController.OverrideUserInterfaceStyle = Application.Current.UserAppTheme switch
        {
            AppTheme.Light => UIKit.UIUserInterfaceStyle.Light,
            AppTheme.Dark => UIKit.UIUserInterfaceStyle.Dark,
            _ => UIKit.UIUserInterfaceStyle.Unspecified,
        };

        //UIKit.UIApplication.SharedApplication.Windows[0].OverrideUserInterfaceStyle = UIKit.UIUserInterfaceStyle.Dark;
#endif
    }
}
