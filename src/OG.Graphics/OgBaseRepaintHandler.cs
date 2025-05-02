using OG.Graphics.Abstraction;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Graphics;
public abstract class OgBaseRepaintHandler<TContext> : IOgRepaintHandler where TContext : class, IOgRepaintContext
{
    public bool CanHandle(IOgRepaintContext context) => context is TContext;
    public bool Handle(IOgRepaintContext context) => Handle((context as TContext)!);
    protected abstract bool Handle(TContext reason);
}