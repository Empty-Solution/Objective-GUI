using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
namespace OG.Layout;
public class OgVerticalLayoutTool<TElement>(int spacing) : OgPositioningLayoutTool<TElement>(spacing) where TElement : IOgElement
{
    public override OgRectangle GetRectangle(OgRectangle elementRect, OgRectangle lastRect, int spacing) =>
        new(lastRect.X, lastRect.YMax + spacing, elementRect.Width, elementRect.Height);
}