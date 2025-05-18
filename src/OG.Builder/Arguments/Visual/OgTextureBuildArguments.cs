using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgTextureBuildArguments(string name, Color value, Material material) : OgValueElementBuildArguments<Color>(name, value)
{
    public Material Material => material;
}