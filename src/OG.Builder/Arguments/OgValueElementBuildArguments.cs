namespace OG.Builder.Arguments;
public class OgValueElementBuildArguments<TValue>(string name, TValue initial) : OgElementBuildArguments(name)
{
    public TValue InitialValue => initial;
}