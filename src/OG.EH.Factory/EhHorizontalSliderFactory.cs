using OG.Common.Scoping.Abstraction;
using OG.Element.Abstraction;
using OG.Element.Layout;
using OG.Factory.Arguments;
using OG.Factory.General;
using OG.Factory.Interactive;
using OG.Factory.Visual;
using UnityEngine;

namespace OG.EH.Factory;

//#shitname
public class EhHorizontalSliderFactory(OgImageFactory backgroundFactory, OgImageFactory thumbFactory, OgHorizontalSliderFactory sliderFactory) : OgFactory<IOgElement, EhSliderFactoryArguments>
{
    public override string TypeName { get; } = "EHHorizontalSlider";

    public override IOgElement Create(EhSliderFactoryArguments arguments)
    {
        IOgValueView<IOgElement, IOgTransformScope, float> slider = sliderFactory.Create(new OgSliderFactoryArguments(arguments.Name, arguments.Transform, arguments.Value, arguments.Range, arguments.ScrollStep));
        slider.AddChild(backgroundFactory.Create(new OgTextureFactoryArguments($"{arguments.Name}_background", arguments.Transform, Texture2D.whiteTexture)));
        slider.AddChild(thumbFactory.Create(new OgTextureFactoryArguments($"{arguments.Name}_thumb", arguments.ThumbTransform, Texture2D.whiteTexture)));
        return slider;
    }
}

//#shitname again lol
public class EhLayoutSliderFactory(EhHorizontalSliderFactory sliderFactory, OgLayoutContainerFactory layoutContainerFactory, OgTextFactory textFactory) : OgFactory<IOgElement, EhLayoutSliderFactoryArguments>
{
    public override string TypeName { get; } = "EHLayoutSlider";

    public override IOgElement Create(EhLayoutSliderFactoryArguments arguments)
    {
        IOgContainer<IOgElement> container = layoutContainerFactory.Create(new OgContentFactoryArguments<IOgLayout<IOgElement>>($"{arguments.Name}_layout", arguments.Transform, new OgHorizontalLayout<IOgElement>(arguments.LayoutStep)));
        container.AddChild(textFactory.Create(new OgTextFactoryArguments($"{arguments.Name}_text", arguments.TextTransform, arguments.Text)));
        container.AddChild(sliderFactory.Create(arguments));
        return container;
    }
}