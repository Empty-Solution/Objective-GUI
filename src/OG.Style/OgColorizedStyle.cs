using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Style;

public class OgColorizedStyle(Vector4 offset, Color color) : OgStyle(offset), IOgColorizedStyle
{
    public Color Color { get; set; } = color;
}