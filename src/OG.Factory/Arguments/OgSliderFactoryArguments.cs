using DK.Common.DataTypes.Abstraction;
using OG.Common.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.Arguments;

public class OgSliderFactoryArguments(string name, IOgTransform transform, float value, IDkRange<float> range, float scrollStep)
    : OgRangeValueFactoryArguments<float>(name, transform, value, range), IOgSliderFactoryArguments
{
    public float ScrollStep { get; set; } = scrollStep;
}