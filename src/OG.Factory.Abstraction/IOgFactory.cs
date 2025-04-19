using DK.Common.Factory.Abstraction;
using OG.Element.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgFactory<TElement, TArguments> : IDkFactory<TElement, TArguments> where TElement : IOgElement where TArguments : IOgFactoryArguments
{
    /// <summary>
    ///     XLM HELPER
    /// </summary>
    string TypeName { get; }
}