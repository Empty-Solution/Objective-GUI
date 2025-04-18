using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.Interactive;

public abstract class OgDraggableValueView<TElement, TScope, TValue>(string name, TValue value, TScope rootScope, IOgTransform transform)
    : OgValueView<TElement, TScope, TValue>(name, value, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    protected override void BeginInteract(OgEvent reason)
    {
        base.BeginInteract(reason);
        UpdateValue(reason);
    }

    protected override void EndInteract(OgEvent reason)
    {
        base.EndInteract(reason);
        UpdateValue(reason);
    }

    protected override void HandleMouseDrag(OgEvent reason)
    {
        base.HandleMouseDrag(reason);
        if(!IsInteracting) return;
        UpdateValue(reason);
    }

    protected abstract TValue CalculateValue(OgEvent reason, TValue value);

    private void UpdateValue(OgEvent reason)
    {
        TValue value = Value;
        TValue newValue = CalculateValue(reason, value);
        if(Equals(value, newValue)) return;
        ChangeValue(newValue, reason);
    }
}