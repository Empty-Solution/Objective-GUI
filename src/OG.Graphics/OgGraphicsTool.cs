using DK.Matching;
using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
using OG.Graphics.Abstraction;
using OG.Graphics.Abstraction.Contexts;
using System.Collections.Generic;
namespace OG.Graphics;
public abstract class OgGraphicsTool(IEnumerable<IOgRepaintHandler> handlers) : IOgGraphicsTool
{
    private readonly DkMatcherProvider<IOgRepaintContext, IOgRepaintHandler> m_Provider = new(handlers);
    public DkScopeContext Repaint(IOgRepaintContext context) => Invoke(context);
    public abstract DkScopeContext Clip(OgRectangle rectangle);
    public abstract DkScopeContext Inline(OgRectangle rectangle);
    protected DkScopeContext Invoke(IOgRepaintContext reason)
    {
        if(m_Provider.TryGetMatcher(reason, out IOgRepaintHandler handler)) handler.Handle(reason);
        return new();
    }
}