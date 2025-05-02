using DK.Getting.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using OG.DataTypes.Quaternion.Float;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Scale.Float;
using OG.DataTypes.Size;
using OG.Event.Abstraction;
namespace OG.Element.Abstraction;
public interface IOgElement
{
    IDkGetProvider<string>?        Name         { get; }
    IDkGetProvider<bool>?          IsActive     { get; }
    IDkFieldProvider<OgRectangle>? Rectangle    { get; }
    IDkGetProvider<OgQuaternionF>? Rotation     { get; }
    IDkGetProvider<OgScaleF>?      Scale        { get; }
    IDkGetProvider<OgSize>?        RelativeSize { get; }
    bool Proc(IOgEvent reason);
}