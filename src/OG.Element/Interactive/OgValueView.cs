using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.Interactive;

public abstract class OgValueView<TElement, TScope, TValue>(string name, TValue value, TScope rootScope, IOgTransform transform)
    : OgControl<TElement, TScope>(name, rootScope, transform) where TElement : IOgElement where TScope : IOgTransformScope
{
    public delegate void OgValueChangeHandler(OgValueView<TElement, TScope, TValue> instance, TValue value, OgEvent reason);

    public TValue Value { get; private set; } = value;
    public event OgValueChangeHandler? OnValueChanged;

    protected virtual void ChangeValue(TValue newValue, OgEvent reason)
    {
        Value = newValue;
        OnValueChanged?.Invoke(this, newValue, reason);
    }
}