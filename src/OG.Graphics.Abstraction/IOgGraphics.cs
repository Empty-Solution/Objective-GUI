using DK.Matching.Abstraction;
namespace OG.Graphics.Abstraction;
public interface IOgGraphics<TContext> : IOgGraphics where TContext : IOgGraphicsContext
{
    void ProcessContext(TContext ctx);
}
public interface IOgGraphics : IDkMatcher<IOgGraphicsContext>
{
    void ProcessContext(IOgGraphicsContext ctx);
}