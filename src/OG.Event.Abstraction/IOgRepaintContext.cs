using OG.DataTypes.Rectangle;

namespace OG.Event.Abstraction;

public interface IOgRepaintContext
{
    OgRectangle RenderRect { get; } // rect + style.CalSize(content)
}