using DK.Common.DataTypes.Abstraction;
using OG.Common.Abstraction;
using OG.Factory.Arguments;

public class EhSliderFactoryArguments(
    string name,
    IOgTransform transform,
    float value,
    IDkRange<float> range,
    float scrollStep,
    IOgTransform thumbTransform) : OgSliderFactoryArguments(name, transform, value, range, scrollStep)
{
    public IOgTransform ThumbTransform { get; } = thumbTransform;
}

public class EhLayoutSliderFactoryArguments(
    string name,
    IOgTransform transform,
    float value,
    IDkRange<float> range,
    float scrollStep,
    IOgTransform thumbTransform,
    float layoutStep,
    IOgTransform textTransform,
    string textValue) : EhSliderFactoryArguments(name, transform, value, range, scrollStep, thumbTransform)
{
    public float LayoutStep { get; } = layoutStep;
    public IOgTransform TextTransform { get; } = textTransform;
    public string Text { get; } = textValue;
}