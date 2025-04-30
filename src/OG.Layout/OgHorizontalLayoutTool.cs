#region

using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;

#endregion

namespace OG.Layout;

public class OgHorizontalLayoutTool<TElement>(int spacing) : OgLayoutTool<TElement>(spacing) where TElement : IOgElement
{
    public override OgRectangle GetRectangle(OgRectangle elementRect, OgRectangle lastRect, int spacing) => new(lastRect.XMax + spacing, lastRect.Y, elementRect.Width, elementRect.Height);
}