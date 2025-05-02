using OG.DataTypes.Rectangle;
namespace OG.Graphics.Abstraction.Contexts;
public class OgInlineRepaintContext : IOgRepaintContext
{
    public OgRectangle RepaintRect { get; set; }
}