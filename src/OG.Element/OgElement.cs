using DK.Getting.Abstraction.Generic;
using OG.DataTypes.Quaternion.Float;
using OG.DataTypes.Rectangle;
using OG.DataTypes.Scale.Float;
using OG.Element.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element;

public class OgElement(IOgEventProvider eventProvider) : IOgElement
{
    public IDkGetProvider<string>? Name { get; set; }

    public IDkGetProvider<bool>? IsActive { get; set; }

    public IDkGetProvider<OgRectangle>? Rectangle { get; set; }

    public IDkGetProvider<OgQuaternionF>? Rotation { get; set; }

    public IDkGetProvider<OgScaleF>? Scale { get; set; }

    protected IOgEventProvider Events => eventProvider;

    public bool Proc(IOgEvent reason) => ShouldProc(reason) && InternalProc(reason);

    protected virtual bool InternalProc(IOgEvent reason) => Events.Invoke(reason);

    protected virtual bool ShouldProc(IOgEvent reason) => !reason.IsConsumed && Rectangle is not null;
}
