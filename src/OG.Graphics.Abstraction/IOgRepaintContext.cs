using OG.DataTypes.Rectangle;
namespace OG.Graphics.Abstraction;
public interface IOgRepaintContext
{
    OgRectangle RenderRect { get; } // rect + style.CalSize(content)
}