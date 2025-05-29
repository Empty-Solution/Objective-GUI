using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataTypes.BindType;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Builder.Arguments.Interactive;
public class OgBindableBuildArguments<TValue>(string name, IDkObservableProperty<TValue> value, IDkValueOverride<TValue> valueOverride,
    IDkProperty<SortedSet<KeyCode>> bind, IDkGetProvider<EOgBindType> bindTypeGetProvider) : OgValueElementBuildArguments<TValue>(name, value)
{
    public IDkValueOverride<TValue>        ValueOverride       { get; } = valueOverride;
    public IDkProperty<SortedSet<KeyCode>> Bind                { get; } = bind;
    public IDkGetProvider<EOgBindType>     BindTypeGetProvider { get; } = bindTypeGetProvider;
}