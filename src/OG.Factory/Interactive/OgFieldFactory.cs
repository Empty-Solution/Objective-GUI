using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;
using OG.Style.Abstraction;

namespace OG.Factory.Interactive;

public class OgFieldFactory<TScope>(IOgTextStyle style) : OgFactory<IOgElement, IOgContentFactoryArguments<string, TScope>, TScope> where TScope : IOgTransformScope
{
    public override string TypeName { get; } = "Field";
    public override IOgElement Create(IOgContentFactoryArguments<string, TScope> arguments) =>
        new OgField<IOgElement, TScope>(arguments.Name, arguments.Scope, arguments.Transform, arguments.Content, style);
}