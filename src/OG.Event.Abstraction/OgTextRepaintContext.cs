using OG.DataTypes.Rectangle;
using System.Collections.Generic;

namespace OG.Event.Abstraction;

public struct OgTextRepaintContext(IEnumerable<float> chars, float lineHeight, OgRectangle renderRect) : IOgRepaintContext
{
    public IEnumerable<float> CharsSizes { get; set; } = chars;
    public float              LineHeight { get; set; } = lineHeight;
    public OgRectangle        RenderRect { get; set; } = renderRect;
}