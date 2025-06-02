using DK.Getting.Abstraction.Generic;
using OG.Event.Abstraction;
using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgTextureBuildArguments(string name, IDkGetProvider<Color> value, IOgEventHandlerProvider? provider, IDkGetProvider<Texture2D> texture, Vector4 borderWidths,
    Vector4 borderRadiuses, float imageAspect, bool alphaBlend) : OgVisualElementBuildArguments(name, value, provider)
{
    public Vector4                   BorderWidths   => borderWidths;
    public Vector4                   BorderRadiuses => borderRadiuses;
    public float                     ImageAspect    => imageAspect;
    public bool                      AlphaBlend     => alphaBlend;
    public IDkGetProvider<Texture2D> Texture        => texture;
}