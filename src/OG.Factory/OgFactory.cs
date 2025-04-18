using OG.Element.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory;

public abstract class OgFactory<TElement, TArguments> : IOgFactory<TElement, TArguments> where TElement : IOgElement where TArguments : IOgFactoryArguments
{
    public abstract string TypeName { get; }

    public abstract TElement Create(TArguments arguments);
}
