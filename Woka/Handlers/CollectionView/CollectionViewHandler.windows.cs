using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.Maui.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation.Collections;
using Woka.Utils;
using WListView = Microsoft.UI.Xaml.Controls.ListView;
using WSetter = Microsoft.UI.Xaml.Setter;
using WStyle = Microsoft.UI.Xaml.Style;

namespace Woka.Handlers;

/// <summary>
/// Solves <see href="https://github.com/dotnet/maui/issues/4116">#4116</see>
/// and <see href="https://github.com/dotnet/maui/issues/8387">#8387</see>
/// (Issue #8387 was solved and will be packaged only for .net8 which will be released on
/// November 2023, 6 months from now).
/// </summary>
public class CollectionViewHandler : Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler
{
    protected override IItemsLayout? Layout { get => ItemsView?.ItemsLayout; }

    protected override void ConnectHandler(ListViewBase platformView)
    {
        base.ConnectHandler(platformView);

        platformView.Items.VectorChanged += Items_VectorChanged;

        if (Layout is not null)
            Layout.PropertyChanged += LayoutPropertyChanged;
    }

    protected override void DisconnectHandler(ListViewBase platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.Items.VectorChanged -= Items_VectorChanged;

        if (Layout is not null)
            Layout.PropertyChanged -= LayoutPropertyChanged;
    }

    private void Items_VectorChanged(IObservableVector<object> sender, IVectorChangedEventArgs @event)
    {
        if (sender is ItemCollection item)
        {
            if (item?.Count > 5 && Element.ItemsUpdatingScrollMode == Microsoft.Maui.Controls.ItemsUpdatingScrollMode.KeepLastItemInView)//Number of items on a large screen
                PlatformView.ScrollIntoView(item.LastOrDefault());
        }
    }

    void LayoutPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == GridItemsLayout.SpanProperty.PropertyName)
            UpdateItemsLayoutSpan();
        else if (e.PropertyName == GridItemsLayout.HorizontalItemSpacingProperty.PropertyName || e.PropertyName == GridItemsLayout.VerticalItemSpacingProperty.PropertyName)
            UpdateItemsLayoutItemSpacing();
        else if (e.PropertyName == LinearItemsLayout.ItemSpacingProperty.PropertyName)
            UpdateItemsLayoutItemSpacing();
    }

    void UpdateItemsLayoutSpan()
    {
        var type = ListViewBase.GetType();

        if (type.Name == "FormsGridView")
        {
            var spanProp = type.GetProperty(
                "Span",
                BindingFlags.Public | BindingFlags.Instance
            );

            if (Layout is not null)
                spanProp?.SetValue(ListViewBase, ((GridItemsLayout)Layout).Span);
        }
    }

    static WStyle GetItemContainerStyle(GridItemsLayout layout)
    {
        var h = layout?.HorizontalItemSpacing ?? 0;
        var v = layout?.VerticalItemSpacing ?? 0;
        var margin = WinUIHelpers.CreateThickness(h, v, h, v);

        var style = new WStyle(typeof(GridViewItem));

        style.Setters.Add(new WSetter(FrameworkElement.MarginProperty, margin));
        style.Setters.Add(new WSetter(Control.PaddingProperty, WinUIHelpers.CreateThickness(0)));
        style.Setters.Add(new WSetter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));

        return style;
    }

    static WStyle GetVerticalItemContainerStyle(LinearItemsLayout layout)
    {
        var v = layout?.ItemSpacing ?? 0;
        var margin = WinUIHelpers.CreateThickness(0, v, 0, v);

        var style = new WStyle(typeof(ListViewItem));

        style.Setters.Add(new WSetter(FrameworkElement.MinHeightProperty, 0));
        style.Setters.Add(new WSetter(FrameworkElement.MarginProperty, margin));
        style.Setters.Add(new WSetter(Control.PaddingProperty, WinUIHelpers.CreateThickness(0)));
        style.Setters.Add(new WSetter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));

        return style;
    }

    static WStyle GetHorizontalItemContainerStyle(LinearItemsLayout layout)
    {
        var h = layout?.ItemSpacing ?? 0;
        var padding = WinUIHelpers.CreateThickness(h, 0, h, 0);

        var style = new WStyle(typeof(ListViewItem));

        style.Setters.Add(new WSetter(FrameworkElement.MinWidthProperty, 0));
        style.Setters.Add(new WSetter(Control.PaddingProperty, padding));
        style.Setters.Add(new WSetter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Stretch));

        return style;
    }

    void UpdateItemsLayoutItemSpacing()
    {
        var type = ListViewBase.GetType();

        if (type.Name == "FormsGridView" && Layout is GridItemsLayout gridLayout)
        {
            var itemContainerStyleProp = type.GetProperty(
                "ItemContainerStyle",
                BindingFlags.Public | BindingFlags.Instance
            );

            if (Layout is not null)
            {
                itemContainerStyleProp?.SetValue(
                    ListViewBase,
                    GetItemContainerStyle(gridLayout)
                );
            }
        }

        if (Layout is LinearItemsLayout linearItemsLayout)
        {
            switch (ListViewBase)
            {
                case GridView formsListView:
                    formsListView.ItemContainerStyle = GetVerticalItemContainerStyle(linearItemsLayout);
                    break;
                case WListView listView:
                    listView.ItemContainerStyle = GetHorizontalItemContainerStyle(linearItemsLayout);
                    break;
            }
        }
    }
}