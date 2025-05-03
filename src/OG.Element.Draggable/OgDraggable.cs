using OG.DataTypes.Rectangle;
using OG.DataTypes.Vector;
using OG.Element.Abstraction;
using OG.Element.Control;
using OG.Element.Draggable.Abstraction;
using OG.Event.Abstraction;
namespace OG.Element.Draggable;
public abstract class OgDraggable<TElement>(IOgEventProvider eventProvider)
    : OgControl<TElement>(eventProvider), IOgDraggable<TElement> where TElement : IOgElement
{
    public bool IsDragging => IsControlling;
    public override bool HandleMouseMove(IOgMouseMoveEvent reason)
    {
        if(!base.HandleMouseMove(reason)) return false;
        if(!IsDragging) return false;
        OgVector2   delta = reason.MouseMoveDelta;
        OgRectangle rect  = Rectangle!.Get();
        return Rectangle.Set(Move(rect, delta)) && PerformDrag(reason, rect, delta);
    }
    protected abstract bool        PerformDrag(IOgMouseMoveEvent reason, OgRectangle rect, OgVector2 delta);
    protected virtual  OgRectangle Move(OgRectangle rect, OgVector2 delta) => new(rect.X + delta.X, rect.Y + delta.Y, rect.Width, rect.Height);
}