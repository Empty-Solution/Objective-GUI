using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Factory.Arguments;
public class OgTextureFactoryArguments(string name, IDkGetProvider<Rect> rectGetProvider, IOgEventHandlerProvider? eventProvider,
    IDkGetProvider<Color> colorGetter, IDkGetProvider<Texture2D> texture, IDkGetProvider<Vector4>? borderWidths, IDkGetProvider<Vector4>? borderRadiuses,
    float imageAspect, bool alphaBlend) : OgVisualFactoryArguments(name, rectGetProvider, eventProvider, colorGetter)
{
    public IDkGetProvider<Vector4>?  BorderWidths   { get; } = borderWidths;
    public IDkGetProvider<Vector4>?  BorderRadiuses { get; } = borderRadiuses;
    public float                     ImageAspect    { get; } = imageAspect;
    public bool                      AlphaBlend     { get; } = alphaBlend;
    public IDkGetProvider<Texture2D> Texture        => texture;
}