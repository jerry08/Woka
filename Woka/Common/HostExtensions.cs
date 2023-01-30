using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.LifecycleEvents;
using Woka.Handlers;

#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace Woka;

/// <summary>
/// Host extensions for workarounds
/// </summary>
public static class HostExtensions
{
    /// <summary>
    /// Adds all handlers in Woka.Handlers.*
    /// </summary>
    /// <param name="builder"></param>
    public static MauiAppBuilder ConfigureWorkarounds(this MauiAppBuilder builder)
    {
        builder.ConfigureMauiHandlers(handlers =>
        {
            handlers.AddHandler<Image, ImageHandler>();

#if ANDROID
            handlers.AddHandler<Entry, EntryHandler>();
            handlers.AddHandler<RefreshView, RefreshViewHandler>();
            handlers.AddHandler<CollectionView, CollectionViewHandler>();
#elif WINDOWS
            handlers.AddHandler<CollectionView, CollectionViewHandler>();
#endif
        }).ConfigureLifecycleEvents(events =>
        {
#if ANDROID
            events.AddAndroid(android => android
                .OnCreate((activity, bundle) =>
                {
                    ThemeManager.Apply();
                    ThemeManager.Setup();
                }));
#elif IOS
            events.AddiOS(ios => ios
                .OnActivated((app) =>
                {
                    ThemeManager.Apply();
                    ThemeManager.Setup();
                }));
#endif
        });

        AppendMappings();

        return builder;
    }

    private static void AppendMappings()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, v) =>
        {
#if ANDROID
            // Remove underline:
            handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());

            //Set cursor color
            handler.PlatformView.TextCursorDrawable.SetTint(v.TextColor.ToAndroid());
#endif
        });

        AllowMultiLineTruncation();
    }

    /// <summary>
    /// Taken from <see href="https://github.com/hartez/MultilineTruncate">MultilineTruncate</see>
    /// </summary>
    private static void AllowMultiLineTruncation()
    {
#if ANDROID

        /* 
		 * The default Controls handling of LineBreakMode and MaxLines on Android
		 * only allows single lines when using text truncation. However, combining
		 * setMaxLines() and TextUtils.TruncateAt.END _is_ supported on Android 
		 * (see https://developer.android.com/reference/android/widget/TextView#setEllipsize(android.text.TextUtils.TruncateAt))
		 * 
		 * The following code updates the mappings for Label on Android to support
		 * this scenario. Truncation and max lines both affect the platform setting
		 * of maximum lines, so we need to modify the mappings for both properties. 
		 * We append a second mapping that checks for our target situation (end truncation)
		 * and sets the maximum lines to the target value.
		*/

        static void UpdateMaxLines(Microsoft.Maui.Handlers.LabelHandler handler, ILabel label)
        {
            var textView = handler.PlatformView;
            if (label is Label controlsLabel && textView.Ellipsize == Android.Text.TextUtils.TruncateAt.End)
            {
                if (controlsLabel.MaxLines > 0)
                    textView.SetMaxLines(controlsLabel.MaxLines);
            }
        }

        Label.ControlsLabelMapper.AppendToMapping(
           nameof(Label.LineBreakMode), UpdateMaxLines);

        Label.ControlsLabelMapper.AppendToMapping(
            nameof(Label.MaxLines), UpdateMaxLines);

#endif

#if WINDOWS
        static void UpdateMaxLines(Microsoft.Maui.Handlers.LabelHandler handler, ILabel label)
        {
            var textView = handler.PlatformView;
            if (label is Label controlsLabel && textView.TextTrimming == Microsoft.UI.Xaml.TextTrimming.CharacterEllipsis)
            {
                textView.MaxLines = controlsLabel.MaxLines;
            }

            Label.ControlsLabelMapper.AppendToMapping(
               nameof(Label.LineBreakMode), UpdateMaxLines);

            Label.ControlsLabelMapper.AppendToMapping(
                nameof(Label.MaxLines), UpdateMaxLines);
        }
#endif
    }
}