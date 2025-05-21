using UnityEngine;
using Color = UnityEngine.Color;
namespace OG.Graphics.Abstraction;
public interface IOgGraphicsContext
{
    Rect  RenderRect { get; set; }
    Color Color      { get; }
}
public interface IOgTextureGraphicsContext : IOgGraphicsContext
{
    Texture2D? Texture        { get; }
    Vector4    BorderWidths   { get; }
    Vector4    BorderRadiuses { get; }
    float      ImageAspect    { get; }
    bool       AlphaBlend     { get; }
}
public interface IOgTextGraphicsContext : IOgGraphicsContext
{
    string       Text         { get; }
    Font?        Font         { get; }
    int          FontSize     { get; }
    FontStyle    FontStyle    { get; }
    TextAnchor   Alignment    { get; }
    TextClipping TextClipping { get; }
    bool         WordWrap     { get; }
}