using OG.Common.Scoping.Abstraction;
using OG.Element;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;
using OG.Style.Abstraction;

namespace OG.Factory.Interactive;

public class OgFieldFactory(IOgTextStyle style, IOgTransformScope scope) : OgElementFactory<IOgValueView<IOgElement, IOgTransformScope, string>, IOgContentFactoryArguments<string>>
{
    public override string TypeName { get; } = "Field";

    public override IOgValueView<IOgElement, IOgTransformScope, string> Create(IOgContentFactoryArguments<string> arguments) =>
        new OgField<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform, arguments.Content, style, new OgTextEditor(new OgTextCursorController(style), true));
}