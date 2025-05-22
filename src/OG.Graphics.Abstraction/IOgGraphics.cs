using DK.Matching.Abstraction;
namespace OG.Graphics.Abstraction;
public interface IOgGraphics<TContext> : IOgGraphics where TContext : IOgGraphicsContext
{
    void PushContext(TContext ctx);
}
public interface IOgGraphics : IDkMatcher<IOgGraphicsContext>
{
    void PushContext(IOgGraphicsContext ctx);
    void ProcessContexts();
}