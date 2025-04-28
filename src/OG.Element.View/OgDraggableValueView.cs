using OG.Element.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.View;

public abstract class OgDraggableValueView<TElement, TValue>(IOgEventProvider eventProvider) : OgValueView<TElement, TValue>(eventProvider) where TElement : IOgElement
{
    protected override bool BeginControl(IOgMouseKeyDownEvent reason)
    {
        base.BeginControl(reason);
        UpdateValue(reason);
        return true;
    }

    protected override bool EndControl(IOgMouseKeyUpEvent reason)
    {
        base.EndControl(reason);
        UpdateValue(reason);
        return true;
    }

    protected override bool HandleMouseMove(IOgMouseMoveEvent reason)
    {
        base.HandleMouseMove(reason);
        UpdateValue(reason);
        return true;
    }

    protected abstract TValue CalculateValue(IOgMouseEvent reason, TValue value);

    private void UpdateValue(IOgMouseEvent reason)
    {
        TValue value = Value!.Get();
        TValue newValue = CalculateValue(reason, value);
        if(Equals(value, newValue)) return;
        ChangeValue(newValue);
    }
    
    protected static float Lerp(float a, float b, float t) => a + ((b - a) * Clamp(t, 0f, 1f));
    protected static float Clamp(float value, float min, float max) => value < min ? min : value > max ? max : value;
}