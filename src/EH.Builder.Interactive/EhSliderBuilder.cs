using EH.Builder.DataTypes;
using EH.Builder.Interactive.Base;
using EH.Builder.Options;
using OG.Builder.Contexts;
using OG.DataKit.Processing;
using OG.Element.Abstraction;
using OG.Element.Container.Abstraction;
using OG.Element.Visual;
using OG.Transformer.Options;
namespace EH.Builder.Interactive;
public class EhSliderBuilder(EhConfigProvider provider, EhContainerBuilder containerBuilder, EhBaseTextBuilder textBuilder,
    EhInternalSliderBuilder sliderBuilder, EhInternalBindModalBuilder<float> bindModalBuilder)
{
    public IOgContainer<IOgElement> Build(string name, IEhProperty<float> value, float min, float max, string textFormat, int round, float y)
    {
        EhSliderConfig sliderConfig = provider.SliderConfig;
        IOgContainer<IOgElement> container = containerBuilder.Build($"{name}Container", new OgScriptableBuilderProcess<OgContainerBuildContext>(context =>
        {
            context.RectGetProvider.Options
                   .SetOption(new OgSizeTransformerOption(provider.InteractableElementConfig.Width, provider.InteractableElementConfig.Height))
                   .SetOption(new OgMarginTransformerOption(provider.InteractableElementConfig.HorizontalPadding, y));
        }));
        OgTextElement nameText = textBuilder.BuildStaticText(name, sliderConfig.TextColor, name, sliderConfig.NameTextFontSize, sliderConfig.NameAlignment,
            provider.InteractableElementConfig.Width - sliderConfig.Width, provider.InteractableElementConfig.Height);
        container.Add(nameText);
        container.Add(sliderBuilder.Build(name, value, min, max, textFormat, round, provider.InteractableElementConfig.Width - sliderConfig.Width));
        container.Add(bindModalBuilder.Build(name, provider.InteractableElementConfig.Width - sliderConfig.Width,
            (provider.InteractableElementConfig.Height - (sliderConfig.Height * 2)) / 2, sliderConfig.Width, sliderConfig.Height * 2, value.ValueOverride,
            property =>
            {
                return sliderBuilder.Build(name, property, min, max, textFormat, round,
                    provider.InteractableElementConfig.BindModalWidth - sliderConfig.Width - (provider.InteractableElementConfig.HorizontalPadding * 2));
            }));
        return container;
    }
}