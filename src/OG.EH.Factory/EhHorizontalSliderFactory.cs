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
public class EhHorizontalSliderFactory(OgImageFactory backgroundFactory, OgImageFactory thumbFactory, OgHorizontalSliderFactory sliderFactory) : OgElementFactory<IOgValueView<IOgElement, IOgTransformScope, float>, EhSliderFactoryArguments>
{
    public override string TypeName { get; } = "EHHorizontalSlider";

    public override IOgValueView<IOgElement, IOgTransformScope, float> Create(EhSliderFactoryArguments arguments)
    {
        IOgValueView<IOgElement, IOgTransformScope, float> slider = sliderFactory.Create(new OgSliderFactoryArguments(arguments.Name, arguments.Transform, arguments.Value, arguments.Range, arguments.ScrollStep));
        slider.AddChild(backgroundFactory.Create(new OgTextureFactoryArguments($"{arguments.Name}_background", arguments.Transform, Texture2D.whiteTexture)));
        var thumb = thumbFactory.Create(new OgTextureFactoryArguments($"{arguments.Name}_thumb", arguments.ThumbTransform, Texture2D.whiteTexture));
        slider.OnValueChanged += (instance, value, reason) =>
        {
            var rect = thumb.Transform.LocalRect;
            rect.x = Mathf.Clamp((value - arguments.Range.Min) / (arguments.Range.Max - arguments.Range.Min) * slider.Transform.LocalRect.width, slider.Transform.LocalRect.xMin, slider.Transform.LocalRect.xMax);
            thumb.Transform.LocalRect = rect;
        };
        slider.AddChild(thumb);
        return slider;
    }
}

//#shitname again lol
public class EhLayoutSliderFactory(EhHorizontalSliderFactory sliderFactory, OgLayoutContainerFactory layoutContainerFactory, OgTextFactory textFactory) : OgElementFactory<IOgContainer<IOgElement>, EhLayoutSliderFactoryArguments>
{
    public override string TypeName { get; } = "EHLayoutSlider";

    public override IOgContainer<IOgElement> Create(EhLayoutSliderFactoryArguments arguments)
    {
        IOgContainer<IOgElement> container = layoutContainerFactory.Create(new OgContentFactoryArguments<IOgLayout<IOgElement>>($"{arguments.Name}_layout", arguments.Transform, new OgHorizontalLayout<IOgElement>(arguments.LayoutStep)));
        container.AddChild(textFactory.Create(new OgTextFactoryArguments($"{arguments.Name}_text", arguments.TextTransform, arguments.Text)));
        container.AddChild(sliderFactory.Create(arguments));
        return container;
    }
}