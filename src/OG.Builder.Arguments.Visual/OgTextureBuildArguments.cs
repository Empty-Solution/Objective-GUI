using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgTextureBuildArguments(string name, IDkGetProvider<Color> value, IOgEventHandlerProvider? provider, IDkGetProvider<Texture2D> texture, IDkGetProvider<Vector4>? borderWidths,
    IDkGetProvider<Vector4>? borderRadiuses, float imageAspect, bool alphaBlend) : OgVisualElementBuildArguments(name, value, provider)
{
    public IDkGetProvider<Vector4>? BorderWidths   => borderWidths;
    public IDkGetProvider<Vector4>? BorderRadiuses => borderRadiuses;
    public float                     ImageAspect    => imageAspect;
    public bool                      AlphaBlend     => alphaBlend;
    public IDkGetProvider<Texture2D> Texture        => texture;
} 