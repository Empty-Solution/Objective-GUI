using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Element.Visual.Abstraction;
public interface IOgLineElement : IOgVisualElement
{
    IDkGetProvider<Vector2> StartPosition { get; }
    IDkGetProvider<Vector2> EndPosition { get; }
    IDkGetProvider<float> LineWidth { get; }
}