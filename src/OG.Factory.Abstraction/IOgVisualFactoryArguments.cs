using OG.Common.Scoping.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgVisualFactoryArguments<TContent, TScope> : IOgFactoryArguments<TScope> where TScope : IOgTransformScope
{
    TContent Content { get; }
}