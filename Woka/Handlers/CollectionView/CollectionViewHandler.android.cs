using Microsoft.Maui.Controls;
using AndroidX.RecyclerView.Widget;

namespace Woka.Handlers;

/// <summary>
/// Solves <see href="https://github.com/dotnet/maui/issues/12219">this issue.</see>
/// </summary>
public class CollectionViewHandler : Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler
{
    private IItemsLayout ItemsLayout { get; set; } = default!;

    protected override IItemsLayout GetItemsLayout()
    {
        // Throws exception in certain scenarios ("Virtual view must not be null here")
        //return base.GetItemsLayout();

        return ItemsLayout;
    }

    protected override void ConnectHandler(RecyclerView platformView)
    {
        base.ConnectHandler(platformView);

        ItemsLayout = VirtualView.ItemsLayout;
    }
}