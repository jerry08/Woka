using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Hosting;
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
        builder
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<Image, ImageHandler>();

#if ANDROID
                handlers.AddHandler<RefreshView, RefreshViewHandler>();
                handlers.AddHandler<CollectionView, CollectionViewHandler>();
#elif IOS || MACCATALYST
                handlers.AddHandler<RefreshView, RefreshViewHandler>();
#endif
            })
            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android =>
                    android.OnCreate(
                        (activity, bundle) =>
                        {
                            ThemeManager.Apply();
                            ThemeManager.Setup();
                        }
                    )
                );
#elif IOS
                events.AddiOS(ios =>
                    ios.OnActivated(
                        (app) =>
                        {
                            ThemeManager.Apply();
                            ThemeManager.Setup();
                        }
                    )
                );
#endif
            });

        AppendMappings();

        return builder;
    }

    private static void AppendMappings()
    {
#if WINDOWS
        // Taken from https://github.com/dotnet/maui/issues/14557#issuecomment-1651575327
        Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler.Mapper.AppendToMapping(
            "HeaderAndFooterFix",
            (_, collectionView) =>
            {
                collectionView.AddLogicalChild((Element)collectionView.Header);
                collectionView.AddLogicalChild((Element)collectionView.Footer);
            }
        );
#endif
    }
}
