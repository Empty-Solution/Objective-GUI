using DK.Common.DataTypes.Abstraction;
using OG.Common.Scoping.Abstraction;
using System;

namespace OG.Factory.Abstraction;

public interface IOgRangeValueFactoryArguments<TValue, TScope> : IOgFactoryArguments<TScope> where TScope : IOgTransformScope where TValue : IEquatable<TValue>
{
    TValue Value { get; set; }
    IDkRange<TValue> Range { get; set; }
}