
using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Control.Abstraction;

namespace OG.Element.View.Abstraction;

public interface IOgValueView<TElement, TValue> : IOgControl<TElement> where TElement : IOgElement
{
    IDkFieldProvider<TValue>? Value { get; set; }

    bool ChangeValue(TValue newValue);
}