using DK.Getting.Abstraction.Generic;
using OG.DataTypes.Rectangles;
using OG.DataTypes.Vectors.Float;
using OG.Event.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgElement
{
    IDkGetProvider<string>? Name { get; }

    IDkGetProvider<bool>? IsActive { get; }

    IDkGetProvider<OgRectangle>? Rectangle { get; }

    IDkGetProvider<OgVector3F>? Rotation { get; }

    IDkGetProvider<OgVector3F>? Scale { get; }

    bool Proc(IOgEvent reason);
}