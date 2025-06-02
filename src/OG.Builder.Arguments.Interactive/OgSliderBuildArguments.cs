using DK.Property.Observing.Abstraction.Generic;
namespace OG.Builder.Arguments.Interactive;
public class OgSliderBuildArguments(string name, IDkObservableProperty<float> value, float min, float max)
    : OgValueElementBuildArguments<float>(name, value)
{
    public float Min { get; } = min;
    public float Max { get; } = max;
}