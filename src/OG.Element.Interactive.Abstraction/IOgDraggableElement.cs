using DK.Property.Observing.Abstraction.Generic;
using OG.Element.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive.Abstraction;
public interface IOgDraggableElement<TElement> : IOgInteractiveElement<TElement> where TElement : IOgElement
{
    IDkObservableProperty<Vector2>? DragDelta { get; }
}