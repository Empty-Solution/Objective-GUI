using DK.Common.DataTypes.Abstraction;
using OG.Common.Abstraction;
using OG.Factory.Abstraction;
using System;

namespace OG.Factory.Arguments;

public class OgRangeValueFactoryArguments<TValue>(string name, IOgTransform transform, TValue value, IDkRange<TValue> range) :
    OgElementFactoryArguments(name, transform), IOgRangeValueFactoryArguments<TValue> where TValue : IEquatable<TValue>
{
    public TValue Value { get; set; } = value;
    public IDkRange<TValue> Range { get; set; } = range;
}