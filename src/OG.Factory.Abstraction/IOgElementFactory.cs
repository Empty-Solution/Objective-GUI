using DK.Common.Factory.Abstraction;
using OG.Element.Abstraction;

namespace OG.Factory.Abstraction;

public interface IOgElementFactory<TElement, TArguments> : IOgFactory<TElement, TArguments>
    where TElement : IOgElement where TArguments : IOgElementFactoryArguments;