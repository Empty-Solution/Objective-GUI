using OG.Common.Scoping.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgSliderFactoryArguments<TScope> : IOgRangeValueFactoryArguments<float, TScope> where TScope : IOgTransformScope
{
    float ScrollStep { get; set; }
}