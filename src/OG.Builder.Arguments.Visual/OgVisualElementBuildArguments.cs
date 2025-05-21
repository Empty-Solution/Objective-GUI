using UnityEngine;
namespace OG.Builder.Arguments.Visual;
public class OgVisualElementBuildArguments(string name, Color value) : OgElementBuildArguments(name)
{
    public Color Value { get; } = value;
}