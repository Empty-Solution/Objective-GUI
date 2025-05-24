using DK.Property.Observing.Abstraction.Generic;
using UnityEngine;
namespace OG.Builder.Arguments.Interactive;
public class OgVectorBuildArguments(string name, IDkObservableProperty<Vector2> value) : OgValueElementBuildArguments<Vector2>(name, value);