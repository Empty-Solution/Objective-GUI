using OG.Graphics.Abstraction.Contexts;
namespace OG.Graphics.Abstraction;
public interface IOgGraphicsTool
{
    bool Repaint(IOgRepaintContext context);
}