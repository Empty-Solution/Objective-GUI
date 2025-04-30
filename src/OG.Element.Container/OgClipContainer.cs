#region

using DK.Scoping.Extensions;
using OG.DataTypes.Rectangle;
using OG.Element.Abstraction;
using OG.Event.Abstraction;

#endregion

namespace OG.Element.Container;

public class OgClipContainer<TElement>(IOgEventProvider eventProvider) : OgScopedContainer<TElement>(eventProvider) where TElement : IOgElement
{
    protected override DkScopeContext Scope(IOgRepaintEvent reason, OgRectangle rectangle) => reason.GraphicsTool.Clip(rectangle);
}