using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
namespace OG.Layout;
public abstract class OgPositioningLayoutTool<TElement>(int spacing) : OgLayoutTool<TElement> where TElement : IOgElement
{
    public override void ProcessElement(TElement element, OgRectangle parentRect)
    {
        base.ProcessElement(element, parentRect);
        OgRectangle rect = GetRectangle(element.Rectangle!.Get(), m_LastRectangle, spacing);
        m_LastRectangle = rect;
        _               = element.Rectangle.Set(rect);
    }
    public abstract OgRectangle GetRectangle(OgRectangle elementRect, OgRectangle lastRect, int spacing);
}