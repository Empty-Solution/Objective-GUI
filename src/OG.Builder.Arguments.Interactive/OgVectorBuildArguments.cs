using DK.Observing.Generic;
using UnityEngine;
namespace OG.Builder.Arguments.Interactive;
public class OgVectorBuildArguments(string name, Vector2 value, DkObservable<Vector2> observable)
    : OgValueElementBuildArguments<Vector2>(name, value, observable);