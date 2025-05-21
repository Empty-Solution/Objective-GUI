using DK.Matching.Abstraction;
using UnityEngine;
namespace OG.Graphics.Abstraction;
public interface IOgGraphics<TContext> : IOgGraphics where TContext : IOgGraphicsContext
{
    void Render(TContext ctx);
}
public interface IOgGraphics : IDkMatcher<IOgGraphicsContext>
{
    Vector2 Global { get; set; }
    void Render(IOgGraphicsContext ctx);
}