using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Interactive;
using OG.Factory.Abstraction;
using OG.Factory.General;

namespace OG.Factory.Interactive;

public class OgHorizontalSliderFactory<TScope> : OgFactory<IOgElement, IOgSliderFactoryArguments<TScope>, TScope> where TScope : IOgTransformScope
{
    public override string TypeName { get; } = "HorizontalSlider";
    public override IOgElement Create(IOgSliderFactoryArguments<TScope> arguments) =>
        new OgHorizontalSlider<IOgElement, TScope>(arguments.Name, arguments.Scope, arguments.Transform, arguments.Value, arguments.Range, arguments.ScrollStep);
}