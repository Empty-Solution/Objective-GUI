using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgBlurTextureBuildArguments(string name, Color value, Material material, Vector4 borders, float blurStrength)
    : OgValueElementBuildArguments<Color>(name, value)
{
    public Material Material     => material;
    public Vector4  Borders      => borders;
    public float    BlurStrength => blurStrength;
}