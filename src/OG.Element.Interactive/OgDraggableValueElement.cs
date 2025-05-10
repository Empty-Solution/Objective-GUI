using OG.DataTypes.ElementState;
using OG.Element.Abstraction;
using OG.Event.Abstraction;
using OG.Event.Prefab.Abstraction;
namespace OG.Element.Interactive;
public abstract class OgDraggableValueElement<TElement, TValue>(string name, IOgEventHandlerProvider provider)
    : OgInteractableValueElement<TElement, TValue>(name, provider) where TElement : IOgElement
{
    public override bool Invoke(IOgMouseMoveEvent reason) => base.Invoke(reason) || UpdateValue(reason);
    protected override bool BeginControl(IOgMouseKeyDownEvent reason) => base.BeginControl(reason) || UpdateValue(reason);
    protected override bool EndControl(IOgMouseKeyUpEvent reason) => base.EndControl(reason) || UpdateValue(reason);
    protected abstract TValue CalculateValue(IOgMouseEvent reason, TValue value);
    private bool UpdateValue(IOgMouseEvent reason)
    {
        TValue value    = Value!.Get();
        TValue newValue = CalculateValue(reason, value);
        if(Equals(value, newValue)) return false;
        State!.Set(EOgElementState.ACTIVE);
        Value!.Set(newValue);
        return true;
    }
}