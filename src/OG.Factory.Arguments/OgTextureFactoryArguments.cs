using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextureFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkGetProvider<Color> colorGetter, Texture2D texture, Vector4 borderWidths, Vector4 borderRadiuses, float imageAspect, bool alphaBlend)
    : OgVisualFactoryArguments(name, rectGetProvider, eventProvider, colorGetter)
{
    public Vector4   BorderWidths   { get; } = borderWidths;
    public Vector4   BorderRadiuses { get; } = borderRadiuses;
    public float     ImageAspect    { get; } = imageAspect;
    public bool      AlphaBlend     { get; } = alphaBlend;
    public Texture2D Texture        => texture;
}