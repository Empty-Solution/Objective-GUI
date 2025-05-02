using OG.DataTypes.Rectangle;
namespace OG.Graphics.Abstraction.Contexts;
public interface IOgRepaintContext
{
    OgRectangle RepaintRect { get; } // rect + style.CalSize(content)
}