using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.DataTypes.Quaternion.Float;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Scale.Float;
using OG.Event.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgElement
{
    IDkGetProvider<string>? Name { get; }

    IDkGetProvider<bool>? IsActive { get; }

    IDkProperty<OgRectangle>? Rectangle { get; }

    IDkGetProvider<OgQuaternionF>? Rotation { get; }

    IDkGetProvider<OgScaleF>? Scale { get; }

    bool Proc(IOgEvent reason);
}