using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgVerticalSliderFactory(IOgTransformScope scope) : OgElementFactory<IOgValueView<IOgElement, IOgTransformScope, float>, IOgSliderFactoryArguments>
{
    public override string TypeName { get; } = "VerticalSlider";

    public override IOgValueView<IOgElement, IOgTransformScope, float> Create(IOgSliderFactoryArguments arguments) =>
        new OgVerticalSlider<IOgElement, IOgTransformScope>(arguments.Name, scope, arguments.Transform, arguments.Value, arguments.Range, arguments.ScrollStep);
}