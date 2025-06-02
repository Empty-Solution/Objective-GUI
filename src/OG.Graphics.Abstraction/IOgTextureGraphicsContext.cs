using UnityEngine;
namespace OG.Graphics.Abstraction;
public interface IOgTextureGraphicsContext : IOgGraphicsContext
{
    Texture2D? Texture        { get; }
    Vector4    BorderWidths   { get; }
    Vector4    BorderRadiuses { get; }
    float      ImageAspect    { get; }
    bool       AlphaBlend     { get; }
    Color      Color          { get; }
}