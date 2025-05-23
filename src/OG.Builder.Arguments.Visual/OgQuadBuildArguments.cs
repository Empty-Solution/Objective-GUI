using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgQuadBuildArguments(string name, Color topLeft, Color topRight, Color bottomLeft, Color bottomRight) : OgElementBuildArguments(name)
{
    public Color TopLeft     { get; } = topLeft;
    public Color TopRight    { get; } = topRight;
    public Color BottomLeft  { get; } = bottomLeft;
    public Color BottomRight { get; } = bottomRight;
}