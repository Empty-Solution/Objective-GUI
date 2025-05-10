using DK.DataTypes.Abstraction;
using OG.Element.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive.Abstraction;
public interface IOgVectorValueElement<TElement> : IOgInteractableValueElement<TElement, Vector2> where TElement : IOgElement
{
    IDkReadOnlyRange<Vector2>? Range { get; }
}