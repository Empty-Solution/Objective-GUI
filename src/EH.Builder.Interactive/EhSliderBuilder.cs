using DK.Getting.Abstraction.Generic;
using EH.Builder.Config;
using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Interactive.Internal;
using EH.Builder.Providing.Abstraction;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Interactive.Abstraction;
using OG.Element.Visual;
using OG.Element.Visual.Abstraction;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhSliderBuilder(IEhConfigProvider provider, EhContainerBuilder containerBuilder, EhBaseTextBuilder textBuilder,
    EhInternalSliderBuilder sliderBuilder, EhInternalBindModalBuilder<float> bindModalBuilder)
{
    public IEhSlider Build(IDkGetProvider<string> name, IEhProperty<float> value, float min, float max, string textFormat, int round, float y)
    {
        EhSliderConfig sliderConfig = provider.SliderConfig;
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name.Get()}Container",
            new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
            {
                context.RectGetProvider.Options
                       .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                       .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
            }));
        OgTextElement nameText = textBuilder.Build(name.Get(), sliderConfig.TextColor, name, sliderConfig.NameTextFontSize, sliderConfig.NameTextAlignment,
            provider.InteractableElementConfig.Width - sliderConfig.Width, provider.InteractableElementConfig.Height);
        container.Add(nameText);
        IOgSlider<IOgVisualElement> slider = sliderBuilder.Build(name.Get(), value, min, max, textFormat, round,
            provider.InteractableElementConfig.Width - sliderConfig.Width);
        container.Add(slider);
        container.Add(bindModalBuilder.Build(name.Get(), provider.InteractableElementConfig.Width - sliderConfig.Width,
            (provider.InteractableElementConfig.Height - (sliderConfig.Height * 2)) / 2, sliderConfig.Width, sliderConfig.Height * 2, value,
            property => sliderBuilder.Build(name.Get(), property, min, max, textFormat, round,
                provider.InteractableElementConfig.BindModalWidth - sliderConfig.Width - (provider.InteractableElementConfig.HorizontalPadding * 2))));
        return new EhSlider(container);
    }
}