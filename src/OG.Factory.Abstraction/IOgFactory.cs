using DK.Common.Factory.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgFactory<TElement, TArguments> : IDkFactory<TElement, TArguments>
    where TArguments : IDkFactoryArguments
{
    /// <summary>
    ///     XLM HELPER
    /// </summary>
    string TypeName { get; }
}