using OG.Style.Abstraction;
using UnityEngine;

namespace OG.Style;

public class OgTextureStyle(Vector4 offset, Color color, Vector4 borderRadiuses, Vector4 borderWidths, 
    float imageAspect = 1f, bool alphaBlend = false, ScaleMode scaleMode = ScaleMode.StretchToFill) : OgColorizedStyle(offset, color), IOgTextureStyle
{
    public ScaleMode ScaleMode { get; set; } = scaleMode;
    public bool AlphaBlend { get; set; } = alphaBlend;
    public float ImageAspect { get; set; } = imageAspect;
    public Vector4 BorderWidths { get; set; } = borderWidths;
    public Vector4 BorderRadiuses { get; set; } = borderRadiuses;
}