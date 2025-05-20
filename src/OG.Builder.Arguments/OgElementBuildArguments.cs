using OG.Builder.Abstraction;
namespace OG.Builder.Arguments;
public class OgElementBuildArguments(string name) : IOgBuildArguments
{
    public string Name => name;
}