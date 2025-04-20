using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Factory.Abstraction;

namespace OG.Factory.General;

public abstract class OgFactory<TElement, TArguments, TScope> : IOgFactory<TElement, TArguments, TScope>
    where TElement : IOgElement where TScope : IOgTransformScope where TArguments : IOgFactoryArguments<TScope>
{
    public abstract string TypeName { get; }

    public abstract TElement Create(TArguments arguments);
}