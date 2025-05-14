using OG.Element.Abstraction;
using UnityEngine;
namespace OG.Element.Visual.Abstraction;
public interface IOgVisualElement : IOgElement
{
    public Color Color { get; set; }
}