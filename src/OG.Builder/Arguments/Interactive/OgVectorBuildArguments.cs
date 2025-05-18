using UnityEngine;
namespace OG.Builder.Arguments.Interactive;
public class OgVectorBuildArguments(string name, Vector2 initial) : OgValueElementBuildArguments<Vector2>(name, initial);