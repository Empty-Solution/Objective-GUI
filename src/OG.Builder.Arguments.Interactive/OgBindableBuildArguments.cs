using DK.Getting.Overriding.Abstraction.Generic;
using DK.Property.Abstraction.Generic;
using DK.Property.Observing.Abstraction.Generic;
using UnityEngine;
namespace OG.Builder.Arguments.Interactive;
public class OgBindableBuildArguments<TValue>(string name, IDkFieldProvider<TValue> value, IDkValueOverride<TValue> valueOverride,
    IDkProperty<KeyCode> bind) : OgElementBuildArguments(name)
{
    public IDkValueOverride<TValue>      ValueOverride { get; } = valueOverride;
    public IDkFieldProvider<TValue> Value         => value;
    public IDkProperty<KeyCode>          Bind          { get; } = bind;
}