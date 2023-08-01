using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using Woka.Layouts;

namespace Woka.Controls;

public class WokaGrid : Grid
{
    protected override ILayoutManager CreateLayoutManager()
        => new WokaGridLayoutManager(this);
}