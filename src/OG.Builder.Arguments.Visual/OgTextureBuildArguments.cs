using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgTextureBuildArguments(string name, Color value, Texture2D texture, Vector4 borderWidths, Vector4 borderRadiuses, float imageAspect,
    bool alphaBlend) : OgVisualElementBuildArguments(name, value)
{
    public Vector4   BorderWidths   => borderWidths;
    public Vector4   BorderRadiuses => borderRadiuses;
    public float     ImageAspect    => imageAspect;
    public bool      AlphaBlend     => alphaBlend;
    public Texture2D Texture        => texture;
}