using DK.Getting.Abstraction;
using DK.Getting.Abstraction.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
namespace EH.Builder.DataTypes;
public class EhValueOverride<TValue>(IDictionary<object?, IDkGetProvider<TValue>> providers, IEhProperty<TValue> linkedProperty) : IEhValueOverride<TValue>
{
    public bool IsOverriden => providers.Count > 0;
    public TValue Get() => providers.Values.First().Get();
    public void Override(object? ovState, IDkGetProvider<TValue> getter)
    {
        providers.Add(ovState, getter);
        linkedProperty.Notify(Get());
    }
    public void Override(object? ovState, IDkGetProvider getter)
    {
        if(getter is not IDkGetProvider<TValue> castedGetter) throw new InvalidCastException();
        Override(ovState, castedGetter);
    }
    public void Revert(object? ovState) => providers.Remove(ovState);
    object IDkGetProvider.Get() => Get()!;
}