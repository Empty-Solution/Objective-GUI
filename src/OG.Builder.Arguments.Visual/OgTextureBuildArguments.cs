using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgTextureBuildArguments(string name, Color value, Material material, Vector4 borders) : OgValueElementBuildArguments<Color>(name, value)
{
    public Material Material => material;
    public Vector4  Borders  => borders;
}