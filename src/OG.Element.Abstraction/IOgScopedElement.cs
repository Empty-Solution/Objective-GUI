using DK.Scoping.Abstraction;

namespace OG.Element.Abstraction;

public interface IOgScopedElement<TScope> : IOgElement where TScope : IDkScope
{
    TScope Scope { get; }
}