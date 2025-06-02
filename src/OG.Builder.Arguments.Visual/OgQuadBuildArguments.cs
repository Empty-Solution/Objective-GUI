using DK.Getting.Abstraction.Generic;
using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgQuadBuildArguments(string name, IDkGetProvider<Color> topLeft, IDkGetProvider<Color> topRight, IDkGetProvider<Color> bottomLeft,
    IDkGetProvider<Color> bottomRight, IDkGetProvider<Material?> material) : OgElementBuildArguments(name)
{
    public IDkGetProvider<Color> TopLeft     { get; } = topLeft;
    public IDkGetProvider<Color> TopRight    { get; } = topRight;
    public IDkGetProvider<Color> BottomLeft  { get; } = bottomLeft;
    public IDkGetProvider<Color> BottomRight { get; } = bottomRight;
    public IDkGetProvider<Material?> Material    { get; } = material;
}