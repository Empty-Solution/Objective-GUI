using OG.Common.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.Arguments;

public class OgContentFactoryArguments<TContent>(string name, IOgTransform transform, TContent content) : OgElementFactoryArguments(name, transform), IOgContentFactoryArguments<TContent>
{
    public TContent Content { get; } = content;
}