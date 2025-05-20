namespace OG.Builder.Arguments;
public class OgValueElementBuildArguments<TValue>(string name, TValue value) : OgElementBuildArguments(name)
{
    public TValue Value => value;
}