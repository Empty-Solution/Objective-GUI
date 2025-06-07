using DK.Getting.Abstraction.Generic;
using OG.Graphics.Abstraction;
using UnityEngine;
namespace OG.Element.Visual.Abstraction;
public interface IOgTextElement : IOgVisualElement
{
    IDkGetProvider<string>? Text         { get; set; }
    IDkGetProvider<float>?  OutlineSize  { get; set; }
    IDkGetProvider<Color>?  OutlineColor { get; set; }
    IOgTextGraphicsContext? Context      { get; }
    Vector2 CalculateSize();
}