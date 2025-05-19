using OG.Element.Abstraction;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Element.Visual.Abstraction;
public interface IOgVisualElement : IOgElement
{
    public Color              Color   { get; set; }
    public IOgGraphicsContext Context { get; }
}