using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Woka.Utils.Extensions;

public static class NavigationExtensions
{
    public static async Task PushModalAsyncSingle(
        this INavigation nav,
        Page page,
        bool animated = true
    )
    {
        if (
            Shell.Current.Navigation.ModalStack.Count == 0
            || Shell.Current.Navigation.ModalStack.Last()?.GetType() != page.GetType()
        )
        {
            await nav.PushModalAsync(page, animated);
        }
    }

    public static async Task PopModalAsyncSingle(this INavigation nav, bool animated = true)
    {
        if (Shell.Current.Navigation.ModalStack.Count > 0)
            await nav.PopModalAsync(animated);
    }

    public static async Task PushAsyncSingle(this INavigation nav, Page page, bool animated = true)
    {
        if (
            Shell.Current.Navigation.NavigationStack.Count == 0
            || Shell.Current.Navigation.NavigationStack.Last()?.GetType() != page.GetType()
        )
        {
            await nav.PushAsync(page, animated);
        }
    }

    public static async Task GoToAsyncSingle(this Shell shell, ShellNavigationState state)
    {
        if (
            Shell.Current.Navigation.NavigationStack.Count == 0
            || Shell.Current.Navigation.NavigationStack.Last()?.GetType().Name
                != state.Location.OriginalString
        )
        {
            await shell.GoToAsync(state);
        }
    }

    public static async Task GoToAsyncSingle(
        this Shell shell,
        ShellNavigationState state,
        bool animate
    )
    {
        if (
            Shell.Current.Navigation.NavigationStack.Count == 0
            || Shell.Current.Navigation.NavigationStack.Last()?.GetType().Name
                != state.Location.OriginalString
        )
        {
            await shell.GoToAsync(state, animate);
        }
    }

    public static async Task GoToAsyncSingle(
        this Shell shell,
        ShellNavigationState state,
        IDictionary<string, object> parameters
    )
    {
        if (
            Shell.Current.Navigation.NavigationStack.Count == 0
            || Shell.Current.Navigation.NavigationStack.Last()?.GetType().Name
                != state.Location.OriginalString
        )
        {
            await shell.GoToAsync(state, parameters);
        }
    }

    public static async Task GoToAsyncSingle(
        this Shell shell,
        ShellNavigationState state,
        bool animate,
        IDictionary<string, object> parameters
    )
    {
        if (
            Shell.Current.Navigation.NavigationStack.Count == 0
            || Shell.Current.Navigation.NavigationStack.Last()?.GetType().Name
                != state.Location.OriginalString
        )
        {
            await shell.GoToAsync(state, animate, parameters);
        }
    }
}
