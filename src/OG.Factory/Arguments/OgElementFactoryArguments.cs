using OG.Common.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.Arguments;

public class OgElementFactoryArguments(string name, IOgTransform transform) : IOgElementFactoryArguments
{
    public string Name { get; } = name;
    public IOgTransform Transform { get; } = transform;
}