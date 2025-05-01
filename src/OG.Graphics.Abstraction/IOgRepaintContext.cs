using OG.DataTypes.Color;
using OG.DataTypes.Rectangle;
namespace OG.Graphics.Abstraction;
public interface IOgRepaintContext
{
    OgRgbaColor Color { get; }
    OgRectangle RenderRect { get; } // rect + style.CalSize(content)
}