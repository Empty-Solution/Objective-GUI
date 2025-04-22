using OG.Element.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.General;

public abstract class OgElementFactory<TElement, TArguments> : IOgElementFactory<TElement, TArguments>
    where TElement : IOgElement where TArguments : IOgElementFactoryArguments
{
    public abstract string TypeName { get; }

    public abstract TElement Create(TArguments arguments);
}