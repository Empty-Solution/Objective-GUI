using UnityEngine;
namespace OG.Builder.Arguments.Interactive;
public class OgVectorBuildArguments(string name, Vector2 value) : OgValueElementBuildArguments<Vector2>(name, value);