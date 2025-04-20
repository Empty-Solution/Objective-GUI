using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;

namespace OG.Factory;

public class OgScrollFactory<TScope> : OgFactory<IOgElement, IOgFactoryArguments<TScope>, TScope> where TScope : IOgClipTransformScope
{
    public override string TypeName { get; } = "Scroll";
    public override IOgElement Create(IOgFactoryArguments<TScope> arguments) => 
        new OgScroll<IOgElement, TScope>(arguments.Name, arguments.Scope, arguments.Transform);
}