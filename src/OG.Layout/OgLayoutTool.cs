using OG.DataTypes.Rectangle;
using OG.DataTypes.Size;
using OG.Element.Abstraction;
using OG.Layout.Abstraction;
namespace OG.Layout;
public class OgLayoutTool<TElement> : IOgLayoutTool<TElement> where TElement : IOgElement
{
    protected OgRectangle m_LastRectangle;
    public virtual void ResetLayout() => m_LastRectangle = new();
    public virtual void ProcessElement(TElement element, OgRectangle parentRect)
    {
        OgRectangle elementRect = element.Rectangle!.Get();
        if(element.RelativeSize is null) return;
        OgSize relativeSize = element.RelativeSize.Get();
        int    width        = relativeSize.Width * parentRect.Width / 100;
        int    height       = relativeSize.Height * parentRect.Height / 100;
        element.Rectangle.Set(new(elementRect.X, elementRect.Y, width, height));
    }
}