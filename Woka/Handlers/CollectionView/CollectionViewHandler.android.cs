using AndroidX.RecyclerView.Widget;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Handlers.Items;

namespace Woka.Handlers;

/// <summary>
/// Solves <see href="https://github.com/dotnet/maui/issues/12219">this issue.</see>
/// </summary>
public class CollectionViewHandler : Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler
{
    protected override void DisconnectHandler(RecyclerView platformView)
    {
        base.DisconnectHandler(platformView);
        (platformView as IMauiRecyclerView<ReorderableItemsView>)?.TearDownOldElement(VirtualView);
    }
}