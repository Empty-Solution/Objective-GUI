using DK.Matching;
using OG.Graphics.Abstraction;
using OG.Graphics.Abstraction.Contexts;
using System.Collections.Generic;
namespace OG.Graphics;
public abstract class OgGraphicsTool(IEnumerable<IOgRepaintHandler> handlers) : IOgGraphicsTool
{
    private readonly DkMatcherProvider<IOgRepaintContext, IOgRepaintHandler> m_Provider = new(handlers);
    public bool Repaint(IOgRepaintContext context) => Invoke(context);
    protected bool Invoke(IOgRepaintContext reason) => m_Provider.TryGetMatcher(reason, out IOgRepaintHandler handler) && handler.Handle(reason);
}