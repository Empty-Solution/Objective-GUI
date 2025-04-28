using Dk.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Control.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.View.Abstraction;

public interface IOgValueView<TElement, TValue> : IOgControl<TElement> where TElement : IOgElement
{
    IDkProperty<TValue>? Value { get; set; }

    void ChangeValue(TValue newValue, IOgEvent reason);
}