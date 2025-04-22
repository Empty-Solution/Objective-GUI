using UnityEngine;

namespace OG.Style.Abstraction;

public interface IOgTextureStyle : IOgColorizedStyle
{
    ScaleMode ScaleMode { get; set; }

    bool AlphaBlend { get; set; }

    float ImageAspect { get; set; }

    Vector4 BorderWidths { get; set; }

    Vector4 BorderRadiuses { get; set; }
}