using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
namespace OG.Graphics.Abstraction;
public interface IOgGraphicsTool
{
    DkScopeContext Clip(OgRectangle rectangle);
    DkScopeContext Inline(OgRectangle rectangle);
    bool Repaint<TContext>(TContext context) where TContext : IOgRepaintContext;
}