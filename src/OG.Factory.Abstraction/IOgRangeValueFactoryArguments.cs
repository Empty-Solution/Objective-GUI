using DK.Common.DataTypes.Abstraction;
using System;

namespace OG.Factory.Abstraction;

public interface IOgRangeValueFactoryArguments<TValue> : IOgElementFactoryArguments where TValue : IEquatable<TValue>
{
    TValue Value { get; set; }

    IDkRange<TValue> Range { get; set; }
}