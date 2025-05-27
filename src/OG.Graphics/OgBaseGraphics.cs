using OG.Graphics.Abstraction;
namespace OG.Graphics;
public abstract class OgBaseGraphics<TContext> : IOgGraphics<TContext> where TContext : class, IOgGraphicsContext
{
    public bool CanHandle(IOgGraphicsContext value) => value is TContext;
    public void ProcessContext(IOgGraphicsContext ctx) => ProcessContext((ctx as TContext)!);
    public abstract void ProcessContext(TContext context);
}