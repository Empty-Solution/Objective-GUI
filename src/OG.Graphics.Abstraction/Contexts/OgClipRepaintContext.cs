using OG.DataTypes.Rectangle;
namespace OG.Graphics.Abstraction.Contexts;
public class OgClipRepaintContext : IOgRepaintContext
{
    public OgRectangle RepaintRect { get; set; }
}