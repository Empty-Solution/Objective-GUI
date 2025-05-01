using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Layout.Abstraction;

namespace OG.Layout;

public abstract class OgLayoutTool<TElement>(int spacing) : IOgLayoutTool<TElement> where TElement : IOgElement
{
    protected OgRectangle m_LastRectangle;
    public virtual void ResetLayout() => m_LastRectangle = new();

    public void ProcessElement(TElement element)
    {
        OgRectangle rect = GetRectangle(element.Rectangle!.Get(), m_LastRectangle, spacing);
        m_LastRectangle = rect;
        _=element.Rectangle.Set(rect);
    }

    public abstract OgRectangle GetRectangle(OgRectangle elementRect, OgRectangle lastRect, int spacing);
}