using DK.Scoping.Extensions;
using OG.Graphics.Abstraction;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Graphics;
public abstract class OgRepaintHandler<TContext> : IOgRepaintHandler where TContext : class, IOgRepaintContext
{
    public bool CanHandle(IOgRepaintContext context) => context is TContext;
    public DkScopeContext Handle(IOgRepaintContext context) => Handle((context as TContext)!);
    protected abstract DkScopeContext Handle(TContext reason);
}