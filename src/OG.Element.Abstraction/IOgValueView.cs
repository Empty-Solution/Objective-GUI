using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgValueView<TElement, TScope, TValue> : IOgContainer<TElement> where TElement : IOgElement where TScope : IOgTransformScope
{
    delegate void OgValueChangeHandler(IOgValueView<TElement, TScope, TValue> instance, TValue value, OgEvent reason);

    TValue Value { get; }
    public event OgValueChangeHandler? OnValueChanged;
    void ChangeValue(TValue newValue, OgEvent reason);
}