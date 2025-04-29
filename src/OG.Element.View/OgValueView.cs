using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
using OG.Element.Control;
using OG.Element.View.Abstraction;
using OG.Event.Abstraction;

namespace OG.Element.View;

public class OgValueView<TElement, TValue>(IOgEventProvider eventProvider) : OgControl<TElement>(eventProvider), IOgValueView<TElement, TValue>
    where TElement : IOgElement
{
    public IDkFieldProvider<TValue>? Value { get; set; }

    public bool ChangeValue(TValue newValue) => Value?.Set(newValue) ?? false;
}