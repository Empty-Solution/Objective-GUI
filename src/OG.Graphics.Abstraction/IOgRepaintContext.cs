using OG.DataTypes.Rectangle;
namespace OG.Graphics.Abstraction;
public interface IOgRepaintContext
{
    OgRectangle RepaintRect { get; } // rect + style.CalSize(content)
}