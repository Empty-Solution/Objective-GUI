using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Element.Interactive;

public abstract class OgValueView<TElement, TScope, TValue>(string name, TScope scope, IOgTransform transform, TValue value)
    : OgControl<TElement, TScope>(name, scope, transform), IOgValueView<TElement, TScope, TValue> where TElement : IOgElement where TScope : IOgTransformScope
{
    public TValue Value { get; private set; } = value;
    public event IOgValueView<TElement, TScope, TValue>.OgValueChangeHandler? OnValueChanged;

    public virtual void ChangeValue(TValue newValue, OgEvent reason)
    {
        Value = newValue;
        OnValueChanged?.Invoke(this, newValue, reason);
    }
}