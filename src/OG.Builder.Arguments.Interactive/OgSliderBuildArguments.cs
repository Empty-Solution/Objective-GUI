using DK.Observing.Generic;
namespace OG.Builder.Arguments.Interactive;
public class OgSliderBuildArguments(string name, float value, DkObservable<float> observable, float min, float max)
    : OgValueElementBuildArguments<float>(name, value, observable)
{
    public float Min { get; } = min;
    public float Max { get; } = max;
}