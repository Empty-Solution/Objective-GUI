using DK.Common.DataTypes.Abstraction;
using OG.Common.Scoping.Abstraction;
using System;

namespace OG.Factory.Abstraction;

public interface IOgSliderFactoryArguments<TScope> : IOgRangeValueFactoryArguments<float, TScope> where TScope : IOgTransformScope
{
    float ScrollStep { get; set; }
}