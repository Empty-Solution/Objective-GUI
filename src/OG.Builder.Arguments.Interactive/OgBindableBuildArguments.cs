using DK.Getting.Abstraction.Generic;
using DK.Getting.Overriding.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using OG.DataTypes.BindType;
using System.Collections.Generic;
using UnityEngine;
namespace OG.Builder.Arguments.Interactive;
public class OgBindableBuildArguments<TValue>(string name, IDkObservableProperty<TValue> value, IDkValueOverride<TValue> valueOverride,
    IDkProperty<KeyCode?> bind) : OgValueElementBuildArguments<TValue>(name, value)
{
    public IDkValueOverride<TValue>        ValueOverride       { get; } = valueOverride;
    public IDkProperty<KeyCode?> Bind                { get; } = bind;
}