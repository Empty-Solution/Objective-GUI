using DK.Common.Factory.Abstraction;
using OG.Common.Abstraction;
using OG.Common.Scoping.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgFactoryArguments<TScope> : IDkFactoryArguments where TScope : IOgTransformScope
{
    string Name { get; }
    TScope Scope { get; }
    IOgTransform Transform { get; }
}