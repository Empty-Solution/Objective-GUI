using DK.Scoping.Extensions;
using OG.Graphics.Abstraction.Contexts;
namespace OG.Graphics.Abstraction;
public interface IOgGraphicsTool
{
    DkScopeContext Repaint(IOgRepaintContext context);
}