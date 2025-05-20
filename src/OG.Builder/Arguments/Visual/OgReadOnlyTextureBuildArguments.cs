using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgReadOnlyTextureBuildArguments(string name, Color value, Material material, Vector4 borders, Rect rect)
    : OgTextureBuildArguments(name, value, material, borders)
{
    public Rect Rect => rect;
}