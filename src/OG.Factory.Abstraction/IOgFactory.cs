using DK.Common.Factory.Abstraction;
using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgFactory<TElement, TArguments, TScope> : IDkFactory<TElement, TArguments>
    where TElement : IOgElement where TScope : IOgTransformScope where TArguments : IOgFactoryArguments<TScope>
{
    /// <summary>
    ///     XLM HELPER
    /// </summary>
    string TypeName { get; }
}