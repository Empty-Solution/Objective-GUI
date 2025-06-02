using DK.Property.Observing.Abstraction.Generic;
namespace OG.Builder.Arguments;
public class OgValueElementBuildArguments<TValue>(string name, IDkObservableProperty<TValue> value) : OgElementBuildArguments(name)
{
    public IDkObservableProperty<TValue> Value => value;
}