using OG.Common.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.Arguments;

public class OgFactoryArguments(string name, IOgTransform transform) : IOgFactoryArguments
{
    public string Name { get; } = name;
    public IOgTransform Transform { get; } = transform;
}