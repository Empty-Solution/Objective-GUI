using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;

namespace OG.Factory;

public class OgClickableFactory<TScope> : OgFactory<IOgElement, IOgFactoryArguments<TScope>, TScope> where TScope : IOgTransformScope
{
    public override string TypeName { get; } = "Button";
    public override IOgElement Create(IOgFactoryArguments<TScope> arguments) => 
        new OgClickable<IOgElement, TScope>(arguments.Name, arguments.Scope, arguments.Transform);
}