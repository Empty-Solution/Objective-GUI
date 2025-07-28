using DK.DataTypes.Abstraction;
using DK.Getting.Abstraction.Generic;
using OG.Element.Abstraction;
using UnityEngine;
namespace OG.Element.Interactive.Abstraction;
public interface IOgScroll<TElement> : IOgInteractableValueElement<TElement, Vector2> where TElement : IOgElement
{
    IDkGetProvider<IDkReadOnlyRange<Vector2>>? Range            { get; set; }
    float                                      ScrollMultiplier { get; set; }
    float                                      VisualOffset     { get; set; }
    float                                      InputOffset     { get; set; }
}