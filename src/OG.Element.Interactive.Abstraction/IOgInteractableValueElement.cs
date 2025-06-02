using DK.Property.Abstraction.Generic;
using OG.Element.Abstraction;
namespace OG.Element.Interactive.Abstraction;
public interface IOgInteractableValueElement<TElement, TValue> : IOgInteractableElement<TElement> where TElement : IOgElement
{
    IDkFieldProvider<TValue> Value { get; }
}