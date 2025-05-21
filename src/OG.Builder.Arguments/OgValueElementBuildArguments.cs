using DK.Observing.Generic;
namespace OG.Builder.Arguments;
public class OgValueElementBuildArguments<TValue>(string name, TValue value, DkObservable<TValue> observable) : OgElementBuildArguments(name)
{
    public TValue               Value      => value;
    public DkObservable<TValue> Observable => observable;
}