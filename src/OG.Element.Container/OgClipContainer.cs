using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Element.Container;
public class OgClipContainer<TElement>(IOgEventProvider eventProvider) : OgScopedContainer<TElement>(eventProvider) where TElement : IOgElement
{
    protected readonly OgClipRepaintContext m_Context = new();
    protected override DkScopeContext Scope(IOgRepaintEvent reason, OgRectangle rectangle)
    {
        m_Context.RepaintRect = rectangle;
        reason.GraphicsTool.Repaint(m_Context);
        return m_Context.Scope;
    }
}