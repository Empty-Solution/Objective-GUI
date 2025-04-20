using OG.Common.Scoping.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgContentFactoryArguments<TContent, TScope> : IOgFactoryArguments<TScope> where TScope : IOgTransformScope
{
    TContent Content { get; }
}